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
    [Space(5)]
    public TextMeshProUGUI enemyHealthCount;
    //public Image healthBar;
    public Slider enemyHealthBar; //Health Bar Status For the enemy
    public int enemyTotalHealth = 100; //Health Enemy will start with
    public int enemyDamageAmount; //Allowing enemies to do different damage amounts to the player
    public int emycurrentHealth;

    //public float damageAmount = 0.25f; // Reduce the health bar by this amount
    //private float healAmount = 0.5f;// Fill the health bar by this amount

    [Header("ENEMY SHOOTING")]
    [Space(5)]
    [SerializeField] GameObject enemyBullet; //Calling the game object for the enemy bullet
    public Transform bulletspawnpoint;
    public float fireRate; //The rate at which the enemy will fire the bullet (every _ seconds)
    public float nextFire; //When the enemy should fire their bullet again

    // Start is called before the first frame update
    void Start()
    {
        emycurrentHealth = enemyTotalHealth;

        enemyHealthBar.maxValue = enemyTotalHealth; //maximum value that the healthbar will display
        enemyHealthBar.value = emycurrentHealth; //The updating currentvalue of the player's health when they are injured and etc...
       

        player = GameObject.FindGameObjectWithTag("Player");

        //Setting Up the Shooting enemy bullets
        //fireRate = 3f;
        nextFire = Time.time;

    }

    // Update is called once per frame
    void Update()
    {
        enemyHealthBar.value = emycurrentHealth;
        enemyHealthCount.text = "Health " + emycurrentHealth.ToString(); //Update the value of the enemy Health

        //Call the method to let the enemy check if they need to fire or not
        CheckIfTimeToFire();
    }

    public void CheckIfTimeToFire()
    {
        if(Time.time > nextFire)
        {
            Instantiate(enemyBullet, bulletspawnpoint.position, bulletspawnpoint.rotation);
            nextFire = Time.time + fireRate;   
        }
    }

    public void EnemyTakesDamage (int damageToTake)
    {
        emycurrentHealth -= damageToTake;

        if (emycurrentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void OnTriggerEnter(Collider col) //When the player walks into the enemy, they will take damage
    {
        if (col.gameObject.tag == "Player")
        {
            Debug.Log("Ouchie!");
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
