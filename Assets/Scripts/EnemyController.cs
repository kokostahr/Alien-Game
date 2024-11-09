using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
public class EnemyController : MonoBehaviour
{
    //Youtube references
    //Enemy AI Using NavMesh - https://www.youtube.com/watch?v=UjkSFoLxesw

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
    //Calling the FirstPersonController to access the player's health
    public FirstPersonControls firstPersonControls;

    [Header("ENEMY SHOOTING")]
    [Space(5)]
    [SerializeField] GameObject enemyBullet; //Calling the game object for the enemy bullet
    public Transform bulletspawnpoint;
    //public Rigidbody enemyBulletRB; //to help the bullet move!
    public int enemybulletSpeed;
    public float fireRate; //The rate at which the enemy will fire the bullet (every _ seconds)
    public float nextFire; //When the enemy should fire their bullet again

    [Header("ENEMY ATTACKING")]
    [Space(5)]
    public float timeBetweenAttacks; //i think this may be the same as next fire
    bool alreadyAttacked;
    //defining the different states of attacking
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    [Header("ENEMY PATROLLING")]
    [Space(5)]
    //Making use of the NavMesh agent so that the enemy can move and track the player
    public NavMeshAgent navMeshAgent;
    //A transform to detect the player's movements in game
    public Transform playerMovement;
    //Why do we need a layermask ;(. Defining what is the ground & what the player is. IDK what that means yet
    public LayerMask whatIsGround, whatIsPlayer;
    //Patroling settings
    public Vector3 walkingPoint;
    //bool to check if the walkpoint is already set
    bool walkingPointSet;
    public float walkingPointRange;

    [Header("ENEMYE BLEEDING AND SOUNDS")]
    [Space(5)]
    public ParticleSystem bloodSplash;//THE BLOOD SPLASH THINGY
    public AudioSource enemygrowl;


    private void Awake()
    {
        //Going to set the objects to find the player everytime the game starts
        playerMovement = GameObject.Find("MCAnimatedPlayer").transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

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

        //Make sure the BloodSplash Is Not Playing at game start
        bloodSplash.Pause();
    }

    // Update is called once per frame
    void Update()
    {
        enemyHealthBar.value = emycurrentHealth;
        enemyHealthCount.text = "Health " + emycurrentHealth.ToString(); //Update the value of the enemy Health

        //Call the method to let the enemy check if they need to fire or not
        //CheckIfTimeToFire();

        //Checking if the player is in sight and in the attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patrolling(); //If the player is not in range to be seen or for attacking then the enemy should patrol.
        if (playerInSightRange && !playerInAttackRange) ChasePlayer(); //Chase the player if they're range to be seen, but not to be attacked
        if (playerInSightRange && playerInAttackRange) AttackPlayer(); //The enemy should attack and chase the player when they're in range 

        if (navMeshAgent.velocity != Vector3.zero) // Only rotate if moving in appropriate direction
        {
            transform.forward = navMeshAgent.velocity.normalized;
        }
        
      
    }

    public void CheckIfTimeToFire() //For Shooting their Laser Eyes
    {
        if(Time.time > nextFire)
        {
            Instantiate(enemyBullet, bulletspawnpoint.position, bulletspawnpoint.rotation);
            nextFire = Time.time + fireRate;

            Rigidbody enemyBulletRB = enemyBullet.GetComponent<Rigidbody>();
            enemyBulletRB.velocity = bulletspawnpoint.forward * enemybulletSpeed;
        }
    }

    public void EnemyTakesDamage (int damageToTake)
    {
        emycurrentHealth -= damageToTake;
        //Play the blood splash to show that the enemy takes damage
        bloodSplash.Play();
        if (emycurrentHealth == 0)
        {
            if (this.gameObject != null)
            {
                Destroy(this.gameObject);
            }     
        }
    }

    public void OnTriggerEnter(Collider col) //When the player walks into the enemy, they will take damage
    {
        if (col.gameObject.tag == "Player")
        {
            Debug.Log("Ouchie!");

            //Play the enemy growling sound
            enemygrowl.Play();

            //FindObjectOfType<FirstPersonControls>().currentHealth -= enemyDamageAmount;
            firstPersonControls.currentHealth -= enemyDamageAmount;
        }
    }

    public void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            Debug.Log("Bye!");

            //Stop playing the enemy growling sound
            enemygrowl.Stop();
        }
    }

    void Patrolling()
    {
        if (!walkingPointSet) SearchWalkingPoint();

        if (walkingPointSet)
        {
            navMeshAgent.SetDestination(walkingPoint);
        }

        Vector3 distanceToWalkingPoint = transform.position - walkingPoint;

        //When the walking point has been reached
        if (distanceToWalkingPoint.magnitude < 2f)
        {
            walkingPointSet = false;
        }
    }

    void SearchWalkingPoint()
    {
        //Calculating the random point in range of the player. It returns a random value depending on high the walkpoint range is
        float randomZ = Random.Range(-walkingPointRange, walkingPointRange);
        float randomX = Random.Range(-walkingPointRange, walkingPointRange);
       //Nothing for the y because the characters remain on the ground.

        walkingPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        
        //Using a raycast to detect that the player is actually on the ground?
        if (Physics.Raycast(walkingPoint, -transform.up, 2f, whatIsGround))
        {
            walkingPointSet = true;
        }
    }

    void ChasePlayer()
    {
        navMeshAgent.SetDestination(playerMovement.position);
    }

    void AttackPlayer()
    {

        Vector3 newPlayer = playerMovement.position; //plug the Players Position into a Vector3

        newPlayer.y = transform.position.y; //Isolate the Y in the var

        transform.LookAt(newPlayer);

        //Make sure the enemy stops moving
        //navMeshAgent.SetDestination(transform.position);

        //transform.LookAt(playerMovement);

        if (!alreadyAttacked)
        {
            //Adding the attacking code here.
            CheckIfTimeToFire();

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    void ResetAttack()
    {
        alreadyAttacked = false;
    }

    /*
     * //Star's SCRIPT TO MAKE SPIKES HURT PLAYERS. Using it as a reference for the enemy damage


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
