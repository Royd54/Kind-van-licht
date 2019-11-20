using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyCombat : MonoBehaviour
{
    private float enemyHealth = 50f;
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
            Destroy(this.gameObject);
        }
    }

    public void getDamage(float damage){
        enemyHealth -= damage;
        Debug.Log(enemyHealth);
    }
}
