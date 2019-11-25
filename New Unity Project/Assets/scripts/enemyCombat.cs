using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyCombat : MonoBehaviour
{
    public float enemyHealth = 50f;
    private float canAttack;
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
            GameObject.Find("Player").GetComponent<playerCombat>().addXP(10);
            Destroy(this.gameObject);
        }
    }

    public void getDamage(float damage){
        enemyHealth -= damage;
        Debug.Log("enemyHealth: " + enemyHealth);
        canAttack += 25;
        if (canAttack >= 100)
        {
            float strength = Random.Range(1, 7);
            //anim met courantine
            GameObject.Find("Player").GetComponent<playerCombat>().recieveDamage(Mathf.CeilToInt(strength));
        }
    }
}
