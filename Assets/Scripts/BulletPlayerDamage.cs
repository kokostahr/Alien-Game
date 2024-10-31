using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPlayerDamage : MonoBehaviour
{
    #region - MY OLD CODE
    //[Header("PLAYER BULLET DAMAGE SETTINGS")]
    //[Space(5)]
    //public GameObject enemyObject; //Calling the enemy controller so we can access the enemy's health
    //public int playerBulletsDamage; //The amount of damage the player's bullet will do to the enemies


    //// Start is called before the first frame update
    //void Start()
    //{
    //    //GameObject.FindObjectOfType<EnemyController>();
    //}

    //public void OnTriggerEnter(Collider other) //Function that will decrease the enemy's health when the bullet interacts with their colliders
    //{
    //    if (other.gameObject.CompareTag("Enemy"))
    //    {
    //        Debug.Log("KILL 'EM");
    //        if (enemyObject != null)
    //        {
    //            GameObject.FindObjectOfType<EnemyController>();
                
    //            enemyObject.GetComponent<EnemyController>().emycurrentHealth -= playerBulletsDamage; //reduce enemy healths
    //            //destroy the bullet after it has hit the enemy (will add a blood splash)
    //            //Destroy(projectilePrefab, 0.5f);
    //        }
    //    }
    //}
    #endregion

    #region - NewCODe
    [Header("PLAYER BULLET DAMAGE SETTINGS")]
    [Space(5)]
    public int playerBulletsDamage; //The amount of damage the player's bullet will do to the enemies
    //public ParticleSystem bloodSplash;

    //private void Start()
    //{
    //    //Make sure the bloodSplash doesn't play at the start of the game
    //    bloodSplash.Pause();
    //}

    //private void Update()
    //{
    //    //Need a vector that will continously track where the bullet collides with the player. Gosh I'm just guessing this code
    //    Vector3 bloodTracking = this.transform.position;
    //}

    public void OnTriggerEnter(Collider other) //Function that will decrease the enemy's health when the bullet interacts with their colliders
    {

        ////Need a vector that will continously track where the bullet collides with the player. Gosh I'm just guessing this code
        //Vector3 bloodTracking = this.transform.position;

        
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("KILL 'EM");
            

            // Get the EnemyController directly from the "other" object
            EnemyController enemyController = other.gameObject.GetComponent<EnemyController>();

            //PLAY THE BLOOD PARTICLE SYSTEM. Call the enemyController and access the bloodsplash particle effect from its variables.
            //Why are we accessing it on the enemy? Because the bullet is a prefab and we cannot put gameObj into the inspector
            enemyController.bloodSplash.Play();

            if (enemyController != null)
            {
                // Reduce enemy health
                enemyController.EnemyTakesDamage(playerBulletsDamage);
            }

            // Destroy the bullet after it hits the enemy (you can add effects later)
            Destroy(gameObject);

            //StopPlaying the blood splash. Only play it once everytime the bullet collides.
            enemyController.bloodSplash.Stop();
        }
    }
    #endregion
}


//public void OnCollisionEnter(Collision other) //Function that will decrease the enemy's health when the bullet interacts with their colliders
//{
//    if (other.gameObject.CompareTag("Enemy"))
//    {
//        Debug.Log("KILL 'EM");
//        enemyObject.GetComponent<EnemyController>().emycurrentHealth -= playerBulletsDamage; //reduce enemy healths
//        //destroy the bullet after it has hit the enemy (will add a blood splash)
//        //Destroy(projectilePrefab, 0.5f);
//    }
//