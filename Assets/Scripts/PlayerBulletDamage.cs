using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletDamage : MonoBehaviour
{

    [Header("PLAYER BULLET DAMAGE SETTINGS")]
    [Space(5)]
    public GameObject enemyObject; //Calling the enemy controller so we can access the enemy's health


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter(Collision other) //Function that will decrease the enemy's health when the bullet interacts with their colliders
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("KILL 'EM");
            enemyObject.GetComponent<EnemyController>().emycurrentHealth -= playerBulletDamage; //reduce enemy healths
            //destroy the bullet after it has hit the enemy (will add a blood splash)
            //Destroy(projectilePrefab, 0.5f);
        }
    }
}
