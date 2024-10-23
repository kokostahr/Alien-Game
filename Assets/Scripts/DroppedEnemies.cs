using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedEnemies : MonoBehaviour
{
    #region FIRST ATTEMPT
    //[Header("ENEMY INSTANTIATION SETUP")]
    //[Space(5)]
    //public GameObject shootingEnmy;
    ////public GameObject flyingEnmy;
    //public Transform enemySpawnPoint;

    ////Referencing the countdown timer script to make things easier
    //public CountDownTimer CountDownTimer;

    ////[Header("ENEMY MOVEMENT SETUP")]
    ////[Space(5)]
    ////public int basicMoveSpeed;
    ////public float movementRange = 0.429f;  // How far they move to the left and right
    ////private float movementTimer;

    //void Start()
    //{
    //    //movementTimer = 0f; // Initialize the timer for horizontal movement
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    //// Updating the timer for the left-right movement
    //    //movementTimer += Time.deltaTime;

    //    //// Movement logic using a sine wave for left-right motion
    //    //float movementX = Mathf.Sin(movementTimer * basicMoveSpeed) * movementRange;

    //    //// Loop through all spawned enemies and update their position
    //    //foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy")) // Assuming enemies are tagged "Enemy"
    //    //{
    //    //    Vector3 enemyPos = enemy.transform.position;
    //    //    enemy.transform.position = new Vector3(enemySpawnPoint.position.x + movementX, enemyPos.y, enemyPos.z);
    //    //}

    //    //Setting up for an enemy to be spawned at the exact time in game, alien does the dropping in anim
    //    if (CountDownTimer.timeRemaining <= 260f)
    //    {
    //        Instantiate(shootingEnmy, enemySpawnPoint.position, enemySpawnPoint.rotation);
    //    }

    //    if (CountDownTimer.timeRemaining <= 210f)
    //    {
    //        Instantiate(shootingEnmy, enemySpawnPoint.position, enemySpawnPoint.rotation);
    //    }

    //    if (CountDownTimer.timeRemaining <= 161f)
    //    {
    //        Instantiate(shootingEnmy, enemySpawnPoint.position, enemySpawnPoint.rotation);
    //    }

    //    if (CountDownTimer.timeRemaining <= 111f)
    //    {
    //        Instantiate(shootingEnmy, enemySpawnPoint.position, enemySpawnPoint.rotation);
    //    }

    //    if (CountDownTimer.timeRemaining <= 62f)
    //    {
    //        Instantiate(shootingEnmy, enemySpawnPoint.position, enemySpawnPoint.rotation);
    //    }

    //    if (CountDownTimer.timeRemaining <= 10f)
    //    {
    //        Instantiate(shootingEnmy, enemySpawnPoint.position, enemySpawnPoint.rotation);
    //    }
    //}
    #endregion

    #region SECON ATTEMPT
    //[Header("A list of the game objects that need to be set Active")]
    //[Space(5)]
    //public GameObject[] enemies;
    ////public GameObject enemy;
    //public CountDownTimer countDownTimer;

    //[Header("ENEMY MOVEMENT SETUP")]
    //[Space(5)]
    //public int basicMoveSpeed;
    //public float movementRange = 0.429f;  // How far they move to the left and right
    //private float movementTimer;
    //private GameObject randomEnemy;

    //void Start ()
    //{
        
    //    movementTimer = 0f; // Initialize the timer for horizontal movement

    //    foreach (GameObject enemy in enemies)
    //    {
    //        enemy.SetActive(false);
    //    }
    //}

    //void Update ()
    //{
    //    //Updating the timer for the left-right movement
    //    movementTimer += Time.deltaTime;

    //    // Movement logic using a sine wave for left-right motion
    //    float movementX = Mathf.Sin(movementTimer * basicMoveSpeed) * movementRange;

    //    // Loop through all spawned enemies and update their position
    //    foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy")) // Assuming enemies are tagged "Enemy"
    //    {
    //        Vector3 enemyPos = enemy.transform.position;
    //        enemy.transform.position = new Vector3(enemyPos.x + movementX, enemyPos.y, enemyPos.z);
    //    }


    //    //I want to set a random object in the list active. 
    //    GameObject randomEnemy = enemies[Random.Range(0, enemies.Length)];

    //    if (countDownTimer.timeRemaining <= 260f && !randomEnemy.activeSelf)
    //    {
    //        //Instantiate(shootingEnmy, enemySpawnPoint.position, enemySpawnPoint.rotation);
    //        //randomEnemy.SetActive(true && !randomEnemy.activeSelf);
    //        randomEnemy = enemies[Random.Range(0, enemies.Length)];
    //        randomEnemy.SetActive(true);
    //    }

    //    else if (countDownTimer.timeRemaining <= 210f)
    //    {
    //        //Instantiate(shootingEnmy, enemySpawnPoint.position, enemySpawnPoint.rotation);
    //        //randomEnemy.SetActive(true && !randomEnemy.activeSelf);
    //        randomEnemy = enemies[Random.Range(0, enemies.Length)];
    //        randomEnemy.SetActive(true);
    //    }

    //    else if (countDownTimer.timeRemaining <= 161f)
    //    {
    //        //Instantiate(shootingEnmy, enemySpawnPoint.position, enemySpawnPoint.rotation);
    //        //randomEnemy.SetActive(true && !randomEnemy.activeSelf);
    //        randomEnemy = enemies[Random.Range(0, enemies.Length)];
    //        randomEnemy.SetActive(true);
    //    }

    //    else if (countDownTimer.timeRemaining <= 111f)
    //    {
    //        //Instantiate(shootingEnmy, enemySpawnPoint.position, enemySpawnPoint.rotation);
    //        //randomEnemy.SetActive(true && !randomEnemy.activeSelf);
    //        randomEnemy = enemies[Random.Range(0, enemies.Length)];
    //        randomEnemy.SetActive(true);
    //    }

    //    else if (countDownTimer.timeRemaining <= 62f)
    //    {
    //        //Instantiate(shootingEnmy, enemySpawnPoint.position, enemySpawnPoint.rotation);
    //        //randomEnemy.SetActive(true && !randomEnemy.activeSelf);
    //        randomEnemy = enemies[Random.Range(0, enemies.Length)];
    //        randomEnemy.SetActive(true);
    //    }

    //    else if (countDownTimer.timeRemaining <= 10f)
    //    {
    //        //Instantiate(shootingEnmy, enemySpawnPoint.position, enemySpawnPoint.rotation);
    //        //randomEnemy.SetActive(true && !randomEnemy.activeSelf);
    //        randomEnemy = enemies[Random.Range(0, enemies.Length)];
    //        randomEnemy.SetActive(true);
    //    }
    //}



    #endregion

    #region THIRD ATTEMPT

    [Header("A list of the game objects that need to be set Active")]
    [Space(5)]
    public GameObject[] enemies;
    //public GameObject enemy;
    public CountDownTimer countDownTimer;
    public Transform enemySpawnPoint;

    void Start()
    {
        //foreach (GameObject enemy in enemies)
        //{
        //    enemy.SetActive(false);
        //}
    }

    void Update()
    {
        //I want to set a random object in the list active. 
        GameObject randomEnemy = enemies[Random.Range(0, enemies.Length)];

        if (countDownTimer.timeRemaining <= 260f && !randomEnemy.activeSelf)
        {
            Instantiate(enemies[Random.Range(0, enemies.Length)], enemySpawnPoint.position, enemySpawnPoint.rotation);
        }

        if (countDownTimer.timeRemaining <= 210f)
        {
            Instantiate(enemies[Random.Range(0, enemies.Length)], enemySpawnPoint.position, enemySpawnPoint.rotation);
        }

        if (countDownTimer.timeRemaining <= 161f)
        {
            Instantiate(enemies[Random.Range(0, enemies.Length)], enemySpawnPoint.position, enemySpawnPoint.rotation);
        }

        if (countDownTimer.timeRemaining <= 111f)
        {
            Instantiate(enemies[Random.Range(0, enemies.Length)], enemySpawnPoint.position, enemySpawnPoint.rotation);
        }

        if (countDownTimer.timeRemaining <= 62f)
        {
            Instantiate(enemies[Random.Range(0, enemies.Length)], enemySpawnPoint.position, enemySpawnPoint.rotation);
        }

        if (countDownTimer.timeRemaining <= 10f)
        {
            Instantiate(enemies[Random.Range(0, enemies.Length)], enemySpawnPoint.position, enemySpawnPoint.rotation);
        }
    }

    #endregion
}
