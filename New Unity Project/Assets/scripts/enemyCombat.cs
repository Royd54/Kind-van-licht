using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyCombat : MonoBehaviour
{
    public float enemyHealth = 50f;
    public float canAttack;
    private float strength;
    // Start is called before the first frame update
    void Start()
    {
        canAttack = 50f;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyHealth <= 0)
        {
            GameObject.Find(contstantsClass.player).GetComponent<playerCombat>().addXP(10);
            Destroy(this.gameObject);
        }

        if (canAttack >= 100 && GameObject.Find(contstantsClass.player).GetComponent<playerCombat>().attacking == false)
        {
            strength = Random.Range(1, 7);
            Debug.Log(strength);
            //anim met courantine
            if (GameObject.Find(contstantsClass.player).GetComponent<playerCombat>().blocking == true)
            {
                strength /= 2;
                canAttack -= 50;
                Debug.Log(strength /= 2);
            }
            canAttack -= 50;
            GameObject.Find(contstantsClass.player).GetComponent<playerCombat>().recieveDamage(Mathf.CeilToInt(strength));
        }
    }

    public void getDamage(float damage){
        enemyHealth -= damage;
        Debug.Log("enemyHealth: " + enemyHealth);
        canAttack += 30;
    }
}
