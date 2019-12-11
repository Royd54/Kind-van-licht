using System.Collections;
using System.Collections.Generic;
using UnityEngine.Experimental.Rendering.LWRP;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class playerCombat : MonoBehaviour
{
    private float maxPlayerHealth = 20;
    private int maxPlayerMana = 7;

    private float playerHealth;
    private float playerXP;
    private int playerMana;
    private float canAttack;
    private int attackType; // 1 = slash 2 = defend 3 = lightray
    private float minDmg = 0f;
    private float maxDmg = 15f;
    private float strength;
    private float interruptValue;

    [SerializeField] private GameObject slashButton;
    [SerializeField] private GameObject defendButton;
    [SerializeField] private GameObject lightrayButton;
    public bool isChoosingAttack;
    public bool attacking = false;
    public bool choosingAttack = false;
    public bool blocking = false;
    public bool won = false;

    private int enemyCount;
    private int enemyIndex;
    [SerializeField] private GameObject[] enemys;

    [SerializeField] public GameObject chosenEnemy;
    [SerializeField] private Camera mainCamera;

    [SerializeField] private GameObject mainLight;
    [SerializeField] private GameObject dimMainLight;
    [SerializeField] private GameObject playerLight;
    [SerializeField] private GameObject enemyLight;

    [SerializeField] private GameObject slashUIbox;
    [SerializeField] private GameObject defenceUIBox;
    [SerializeField] private GameObject XPbar;
    [SerializeField] private Image XPbarFill;
    private double charge = 0;
    private float maxCharge = 500;

    [SerializeField] private GameObject playerSlider;
    [SerializeField] private float curentUIpos;


    // Start is called before the first frame update
    void Start()
    {
        playerHealth = 40f;
        playerMana = 7;
        canAttack = 70f;
        enemyCount = enemys.Length;
        enemyIndex = 0;
        isChoosingAttack = true;
        Debug.Log(enemys.Length);
    }

    // Update is called once per frame
    void Update()
    {
        //updates the attack slider knob
        curentUIpos = playerSlider.GetComponent<Slider>().value;
        playerSlider.GetComponent<Slider>().value = Mathf.Lerp(curentUIpos, canAttack, Time.deltaTime * 10);

        if (canAttack >= 100)
        {
            canAttack = 100;
        }
        else
        {
            canAttack += 0.1f;
        }

        mainLight.SetActive(true);
        if (enemyIndex <= 0)
        {
            enemyIndex = enemyCount;
        }

        //adds xp if the player has won
        if(won == true)
        {
            addXP();
        }

        if (canAttack == 85)
        {
            StartCoroutine(wait());
        }

            //if the player can attack, the player is able to choose a attack type and attack
            if (curentUIpos >= 90  && won == false)
            {
            
            //if the enemy can't attack asweel then the player is able to attack
            if (isChoosingAttack == true && GameObject.Find("Enemy").GetComponent<enemyCombat>().canAttack < 100)
            {
                chooseAttack();
            }
            else if (isChoosingAttack == false && attacking == false) 
            {
                playerLight.SetActive(false);
                dimMainLight.SetActive(true);
                GetComponentInChildren<colorChanger>().changePlayerColor();
                enemyLight.SetActive(true);
                mainLight.SetActive(false);

                defendButton.SetActive(false);
                slashButton.SetActive(false);
                lightrayButton.SetActive(false);

                //if the player has choosen the attack type via the ui buttons the player can attack with the K button
                if (Input.GetKeyDown(KeyCode.K)) 
                {
                    choosingAttack = false;
                    chosenEnemy = enemys[enemyIndex - 1];
                    Debug.Log(enemyIndex);
                    if (attackType == 1)
                    {
                        isChoosingAttack = false;
                        StartCoroutine(Slash(chosenEnemy));
                    }
                    else if (attackType == 2)
                    {
                        isChoosingAttack = false;
                        StartCoroutine(Block(chosenEnemy));
                    }
                    else
                    {

                    }
                }
                //cycles through the enemy array
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    enemyIndex--;
                    chosenEnemy = enemys[enemyIndex];
                    Debug.Log(enemys[enemyIndex]);
                }
            }
        }
        else // if the player can't attack yet the player needs to wait
        {
            StartCoroutine(canAttackCooldown());
        }

    }

    private IEnumerator Slash(GameObject enemy)
    {
        //deals damage and does the attack animations 
        mainCamera.GetComponent<cameraAnim>().enabled = true;
        mainCamera.GetComponent<cameraAnim>().focusPlayer();
        attacking = true;
        playerLight.SetActive(false);
        dimMainLight.SetActive(false);
        enemyLight.SetActive(false);
        mainLight.SetActive(true);
        GetComponentInChildren<colorChanger>().changePlayerColorMiddle();

        yield return new WaitForSeconds(2);
        GameObject.Find("baked_idle_test_2").GetComponent<Animator>().SetBool("IsAttacking", true);
        mainCamera.GetComponent<cameraAnim>().focusAttack();
        slashUIbox.SetActive(true);

        yield return new WaitForSeconds(2);
        slashUIbox.SetActive(false);
        GameObject.Find("baked_idle_test_2").GetComponent<Animator>().SetBool("IsAttacking", false);
        //deals ROUNDED damage to the enemy
        strength = Random.Range(minDmg, maxDmg);
        enemy.GetComponent<enemyCombat>().getDamage(Mathf.CeilToInt(strength));
        canAttack -= 20f;
        yield return new WaitForSeconds(2);
        


        if (enemy.GetComponent<enemyCombat>().enemyHealth >= 0)
        {
            mainCamera.GetComponent<cameraAnim>().focusRestore();
            yield return new WaitForSeconds(1);
            mainCamera.GetComponent<cameraAnim>().enabled = false;
            isChoosingAttack = true;
            attacking = false;
        }
        
    }

    private IEnumerator Block(GameObject enemy)
    {
        attacking = true;
        blocking = true;
        playerLight.SetActive(false);
        dimMainLight.SetActive(false);
        enemyLight.SetActive(false);
        mainLight.SetActive(true);
        GetComponentInChildren<colorChanger>().changePlayerColorMiddle();

        enemy.GetComponent<enemyCombat>().getDamage(0);
        playerMana -= 2; 
        yield return new WaitForSeconds(2);
        defenceUIBox.SetActive(true);

        yield return new WaitForSeconds(2);
        defenceUIBox.SetActive(false);

        if (enemy.GetComponent<enemyCombat>().enemyHealth >= 0)
        {
            yield return new WaitForSeconds(1);
            mainCamera.GetComponent<cameraAnim>().enabled = false;
            isChoosingAttack = true;
            attacking = false;
        }
    }

    private IEnumerator canAttackCooldown()
    {
        //Debug.Log("needed more attackpoints");
        playerLight.SetActive(false);
        dimMainLight.SetActive(false);
        enemyLight.SetActive(false);
        mainLight.SetActive(true);
        GetComponentInChildren<colorChanger>().changePlayerColorMiddle();

        //adds float until the player can attack again
        yield return new WaitForSeconds(1);

    }

    private IEnumerator wait()
    {
        curentUIpos = playerSlider.GetComponent<Slider>().value = 85;
        yield return new WaitForSeconds(1);
    }

    //attack setter used by the buttons
    public void attackValue(int type)
    {
        isChoosingAttack = false;
        attackType = type;
    }

    //handles light animations
    public void chooseAttack()
    {
        GetComponentInChildren<colorChanger>().changePlayerColorBack();
        playerLight.SetActive(true);
        dimMainLight.SetActive(true);
        enemyLight.SetActive(false);
        mainLight.SetActive(false);

        defendButton.SetActive(true);
        slashButton.SetActive(true);
        lightrayButton.SetActive(true);
        choosingAttack = true;
    }

    public IEnumerator recieveDamage(float damage)
    {
        GameObject.Find("baked_idle_test_2").GetComponent<Animator>().SetBool("IsHit", true);
        if (playerHealth > 0)
        {
            blocking = false;
            playerHealth -= damage;
            choosingAttack = true;
            mainCamera.GetComponent<cameraAnim>().focusPlayer();
            Debug.Log("playerHealth: " + playerHealth);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (damage > 3)
        {
            canAttack -= 50f;
        }

        yield return new WaitForSeconds(2);

        GameObject.Find("baked_idle_test_2").GetComponent<Animator>().SetBool("IsHit", false);
        GameObject.Find("Enemy").GetComponent<Animator>().SetBool("IsAttacking", false);
    }

    //adds XP and starts the XP animations
    public void addXP()
    {
        won = true;
        //xp animatie hiero + camera en ui animatie
        GameObject.Find("baked_idle_test_2").GetComponent<Animator>().SetBool("HasWon", true);
        mainCamera.GetComponent<cameraAnim>().focusXpEarned();
        strength = Random.Range(25, 75);
        canAttack -= Mathf.CeilToInt(interruptValue);
        XPbar.SetActive(true);
        XPbarFill.fillAmount = Mathf.Lerp(XPbarFill.fillAmount, 0.5f, Time.deltaTime * 12);
    }

    //adds the resources if the spirit uses the flowers
    public void addRecourses(float healthPoints, int mana)
    {
        if (playerHealth < maxPlayerHealth)
        {
            playerHealth += healthPoints;
            Debug.Log("playerHealthRecieved: " + healthPoints);
            Debug.Log("totalPlayerHealth: " + playerHealth);
        }
        else { playerHealth = 20; }

        if (playerMana < maxPlayerMana)
        {
            playerMana += mana;
            Debug.Log("manaRevieved: " + mana);
            Debug.Log("totalPlayerMana: " + playerMana);
        }
        else { playerMana = 7; }
    }

    public float getHealth()
    {
        return playerHealth;
    }

    public int getMana()
    {
        return playerMana;
    }

}
