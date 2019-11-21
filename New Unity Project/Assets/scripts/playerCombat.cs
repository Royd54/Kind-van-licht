using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerCombat : MonoBehaviour
{
    private float playerHealth;
    private static float playerXP;
    private int playerMana;
    private float canAttack;
    private int attackType; // 1 = slash 2 = defend 3 = lightray
    private float minDmg = 5f;
    private float maxDmg = 25f;
    private float strength;

    private int enemyCount;
    private int enemyIndex;
    [SerializeField] private GameObject[] enemys;

    [SerializeField] private GameObject chosenEnemy;
    [SerializeField] private Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        playerHealth = 100f;
        playerMana = 7;
        canAttack = 100f;
        enemyCount = enemys.Length;
        enemyIndex = 1;
        Debug.Log(enemys.Length);
    }

    // Update is called once per frame
    void Update()
    {

        if (enemyIndex <= 0)
        {
            enemyIndex = enemyCount;
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            chosenEnemy = enemys[enemyIndex];
            Debug.Log(enemys[enemyIndex]);
            Debug.Log(enemyIndex);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            enemyIndex--;
            chosenEnemy = enemys[enemyIndex];
            Debug.Log(enemys[enemyIndex]);
        }
    }

    private IEnumerator Slash()
    {
        //deals damage and does all the animations
        strength = Random.Range(minDmg, maxDmg);
        chosenEnemy.GetComponent<enemyCombat>().getDamage(Mathf.CeilToInt(strength));
        yield return new WaitForSeconds(1);
    }

    public void chooseAttack()
    {
        StartCoroutine(Slash());
    }

    public void attackCameraZoom()
    {
        //camera animations
    } 

    public void recieveDamage(float damage)
    {
        playerHealth -= damage;
        Debug.Log("playerHealth: " + playerHealth);
    }

    public void addXP(float xp)
    {
        playerXP += xp;
        Debug.Log("playerxp: " + playerXP);
        //xp animatie hiero + camera en ui animatie
    }

}
