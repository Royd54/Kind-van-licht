using System.Collections;
using System.Collections.Generic;
using UnityEngine.Experimental.Rendering.LWRP;
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

    [SerializeField] private GameObject slashButton;
    [SerializeField] private GameObject defendButton;
    [SerializeField] private GameObject lightrayButton;
    public bool isChoosingAttack;
    public bool attacking = false;

    private int enemyCount;
    private int enemyIndex;
    [SerializeField] private GameObject[] enemys;

    [SerializeField] public GameObject chosenEnemy;
    [SerializeField] private Camera mainCamera;

    [SerializeField] private GameObject mainLight;
    [SerializeField] private GameObject dimMainLight;
    [SerializeField] private GameObject playerLight;
    [SerializeField] private GameObject enemyLight;

    // Start is called before the first frame update
    void Start()
    {
        playerHealth = 100f;
        playerMana = 7;
        canAttack = 100f;
        enemyCount = enemys.Length;
        enemyIndex = 0;
        isChoosingAttack = true;
        Debug.Log(enemys.Length);
    }

    // Update is called once per frame
    void Update()
    {
        mainLight.SetActive(true);
        if (enemyIndex <= 0)
        {
            enemyIndex = enemyCount;
        }

        if (isChoosingAttack == true)
        {
            chooseAttack();
        }
        else if(isChoosingAttack == false && attacking == false)
        {
            playerLight.SetActive(false);
            dimMainLight.SetActive(true);
            enemyLight.SetActive(true);
            mainLight.SetActive(false);

            defendButton.SetActive(false);
            slashButton.SetActive(false);
            lightrayButton.SetActive(false);

            if (Input.GetKeyDown(KeyCode.K))
            {
                chosenEnemy = enemys[enemyIndex -1];
                Debug.Log(enemyIndex);
                if (attackType == 1)
                {
                    isChoosingAttack = false;
                    StartCoroutine(Slash(chosenEnemy));
                }
                else if (attackType == 2)
                {

                }
                else
                {

                }
            }

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                enemyIndex--;               
                chosenEnemy = enemys[enemyIndex];
                Debug.Log(enemys[enemyIndex]);
            }
        }
    }

    private IEnumerator Slash(GameObject enemy)
    {
        //deals damage and does the attack animations
        mainCamera.GetComponent<cameraAnim>().focusPlayer();
        attacking = true;
        playerLight.SetActive(false);
        dimMainLight.SetActive(false);
        enemyLight.SetActive(false);
        mainLight.SetActive(true);

        yield return new WaitForSeconds(2);
        mainCamera.GetComponent<cameraAnim>().focusAttack();

        strength = Random.Range(minDmg, maxDmg);
        enemy.GetComponent<enemyCombat>().getDamage(Mathf.CeilToInt(strength));
        yield return new WaitForSeconds(3);

        if (enemy.GetComponent<enemyCombat>().enemyHealth >= 0)
        {
            mainCamera.GetComponent<cameraAnim>().focusRestore();
            yield return new WaitForSeconds(1);
            isChoosingAttack = true;
            attacking = false;
        }
        
    }

    public void attackValue(int type)
    {
        isChoosingAttack = false;
        attackType = type;
    }

    public void chooseAttack()
    {
        playerLight.SetActive(true);
        dimMainLight.SetActive(true);
        enemyLight.SetActive(false);
        mainLight.SetActive(false);

        defendButton.SetActive(true);
        slashButton.SetActive(true);
        lightrayButton.SetActive(true);
    }

    public void recieveDamage(float damage)
    {
        playerHealth -= damage;
        mainCamera.GetComponent<cameraAnim>().focusPlayer();
        Debug.Log("playerHealth: " + playerHealth);
    }

    public void addXP(float xp)
    {
        playerXP += xp;
        //xp animatie hiero + camera en ui animatie
        mainCamera.GetComponent<cameraAnim>().focusXpEarned();
        Debug.Log("playerxp: " + playerXP);
    }

}
