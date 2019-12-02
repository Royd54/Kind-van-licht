using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIHandler : MonoBehaviour
{
    [SerializeField] private Text healthText;
    [SerializeField] private Text manaText;
    private float playerHealth;
    private int playerMana;

    // Update is called once per frame
    void Update()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<playerCombat>().getHealth();
        healthText.text = playerHealth + " / " + 20;

        playerMana = GameObject.FindGameObjectWithTag("Player").GetComponent<playerCombat>().getMana();
        manaText.text = playerMana + " / " + 7;
    }
}
