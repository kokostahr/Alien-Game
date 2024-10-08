using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeDamage : MonoBehaviour
{
    [Header("PLAYER KNIFE DAMAGE SETTINGS")]
    [Space(5)]
    public int playerKnifeDamage; //The amount of damage the player's knife will do to the enemies

    public void OnTriggerEnter(Collider other) //Function that will decrease the enemy's health when the bullet interacts with their colliders
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("SLASH 'EM UP");

            // Get the EnemyController directly from the "other" object
            EnemyController enemyController = other.gameObject.GetComponent<EnemyController>();

            if (enemyController != null)
            {
                // Reduce enemy health
                enemyController.EnemyTakesDamage(playerKnifeDamage);
            } 
        }
    }
}
