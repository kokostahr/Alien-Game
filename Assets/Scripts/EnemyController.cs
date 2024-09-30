using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{

    [Header("PLAYER SETTINGS 4 ENEMY")]
    private GameObject player;

    [Header("ENEMY HEALTH SETTINGS")]
    public TextMeshProUGUI enemyHealthCount;
    //public Image healthBar;
    public Slider enemyHealthBar; //Health Bar Status For the enemy
    public int enemyTotalHealth = 100; //Health Enemy will start with
    public int enemyDamageAmount; //Allowing enemies to do different damage amounts to the player
    private int emycurrentHealth;

    //public float damageAmount = 0.25f; // Reduce the health bar by this amount
    //private float healAmount = 0.5f;// Fill the health bar by this amount

    

    // Start is called before the first frame update
    void Start()
    {
        emycurrentHealth = enemyTotalHealth;

        enemyHealthBar.maxValue = enemyTotalHealth; //maximum value that the healthbar will display
        enemyHealthBar.value = emycurrentHealth; //The updating currentvalue of the player's health when they are injured and etc...
       

        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        enemyHealthBar.value = emycurrentHealth;
        enemyHealthCount.text = "Health " + emycurrentHealth.ToString(); //Update the value of the enemy Health
    }

    public void EnemyTakesDamage (int damageToTake)
    {
        emycurrentHealth -= damageToTake;

        if (emycurrentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            FindObjectOfType<FirstPersonControls>().currentHealth -= enemyDamageAmount;
        }
    }



    /*
     * //A SCRIPT TO MAKE SPIKES HURT PLAYERS


        public int spikeDamage = 1;
        
        private GameObject player;
        private PlayerHealth playerHealth;

    void Awake() 
    {
        player = GameObject.FindGameObjectWithTag("Player")
        playerHealth = player.GetComponent<PlayerHealth>();
    }

//DONT FORGET TO PUT COLLIDERS ON YOUR SPIKES AND CHECK THE "isTrigger' BOX
//TO MAKE THEM TRIGGERS
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject == "Player")
        {
            Debug.Log("Ouch!)
            playerHealth.LooseHealth(spikeDamage)
        }
}
     */
}
