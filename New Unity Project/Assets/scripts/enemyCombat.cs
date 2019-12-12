using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class enemyCombat : MonoBehaviour
{
    public float enemyHealth;
    public float canAttack;
    private float strength;
    [SerializeField] private GameObject enemySlider;
    [SerializeField] private GameObject DamageText;

    [SerializeField] private float curentUIpos;
    [SerializeField] private Animator frontLegAnim;
    [SerializeField] private Animator backLegAnim;
    [SerializeField] private Animator bodyAnim;
    [SerializeField] private Animator otherAnim;
    // Start is called before the first frame update
    void Start()
    {
        canAttack = 50f;
        enemyHealth = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<Animator>().GetBool("IsAttacking") == false && GetComponent<Animator>().GetBool("IsStunned") == false && GetComponent<Animator>().GetBool("IsDead") == false)
        {
            otherAnim.enabled = false;
            frontLegAnim.enabled = true;
            backLegAnim.enabled = true;
            bodyAnim.enabled = true;
        }
        else
        {
            otherAnim.enabled = true;
            frontLegAnim.enabled = false;
            backLegAnim.enabled = false;
            bodyAnim.enabled = false;
        }

        enemySlider.GetComponent<Slider>().value = canAttack;
        if (canAttack == 80)
        {
            StartCoroutine(wait());
        }

        if (canAttack < 80)
        {
            StartCoroutine(canAttackCooldown());
        }

        //updates the position of the attack UI knob
        curentUIpos = enemySlider.GetComponent<Slider>().value;
        enemySlider.GetComponent<Slider>().value = Mathf.Lerp(curentUIpos, canAttack, Time.deltaTime * 0.5f);
        //enemySlider.GetComponent<Slider>().value = Mathf.MoveTowards(enemySlider.GetComponent<Slider>().value, canAttack, 100.0f);

        //if the enemy is dead the player XP is added
/*        if (enemyHealth <= 0)
        {
            GameObject.Find(contstantsClass.player).GetComponent<playerCombat>().addXP();
            GetComponent<Animator>().SetBool("IsDead", true);
            Destroy(this.gameObject, 3f);
        }*/

        //if the enemy can attack it deals damage to the player
        if (canAttack >= 80 && GameObject.Find(contstantsClass.player).GetComponent<playerCombat>().attacking == false && GameObject.Find(contstantsClass.player).GetComponent<playerCombat>().choosingAttack == false)
        {
            if (GetComponent<Animator>().GetBool("IsStunned") == true)
            {
                GetComponent<Animator>().SetBool("IsStunned", false);
                GetComponent<Animator>().SetBool("IsAttacking", true);
            }
            GetComponent<Animator>().SetBool("IsAttacking", true);
            strength = Random.Range(1, 7);
            //anim met courantine
            if (GameObject.Find(contstantsClass.player).GetComponent<playerCombat>().blocking == true)
            {
                strength /= 2;
                canAttack -= 50;
                Debug.Log(strength /= 2);
            }
            GameObject.Find(contstantsClass.player).GetComponent<playerCombat>().StartCoroutine(GameObject.Find(contstantsClass.player).GetComponent<playerCombat>().recieveDamage(Mathf.CeilToInt(strength)));
            //GameObject.Find(contstantsClass.player).GetComponent<playerCombat>().recieveDamage(Mathf.CeilToInt(strength));
        }
    }

    public IEnumerator wait()
    {
        enemySlider.GetComponent<Slider>().value = 92;
        curentUIpos = enemySlider.GetComponent<Slider>().value;
        yield return new WaitForSeconds(1);
    }

    private IEnumerator canAttackCooldown()
    {
        //adds float until the player can attack again
        if (canAttack >= 100)
        {
            canAttack = 100;
        }
        else
        {
            canAttack += 0.1f;
        }
        yield return new WaitForSeconds(1);
    }

    //getter for the damage taken by the player and adds canAttack, so it can attack faster
    public void getDamage(float damage){
        StartCoroutine(floatingNumber());
        enemyHealth -= damage;
        Debug.Log("enemyHealth: " + enemyHealth);
        if (damage > 7)
        {
            canAttack -= 50f;
        }


    }


    private IEnumerator floatingNumber()
    {
        DamageText.SetActive(true);
        GetComponent<Animator>().SetBool("IsDead", true);
        yield return new WaitForSeconds(3);
        Destroy(this.gameObject);
        GameObject.Find(contstantsClass.player).GetComponent<playerCombat>().addXP();
    }
}
