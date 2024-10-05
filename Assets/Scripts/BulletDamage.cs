using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDamage : MonoBehaviour
{
    [Header("ENEMY SHOOTING SETTINGS")]
    [Space(5)]
    public int enemyBulletDamage;
    public float enemyBulletSpeed;

    Rigidbody bulletRb; //Calling the rigidbody on the enemy bullet
    GameObject target; //Where\who we want the bullet to be shot at
    FirstPersonControls firstPersonControls; //Calling the FPControls script here to access the player's current health
    Vector3 moveDirection; 

    // Start is called before the first frame update
    void Start()
    {
        bulletRb = GetComponent<Rigidbody>();
        target = GameObject.FindGameObjectWithTag("Player");
        
        if (target != null)
        {
            firstPersonControls = target.GetComponent<FirstPersonControls>();
        }
        moveDirection = (target.transform.position - transform.position).normalized * enemyBulletSpeed;
        bulletRb.velocity = new Vector3(moveDirection.x, moveDirection.y, moveDirection.z);
        Destroy(this.gameObject, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("YOUVE BEEN SHOT!");
            if (target != null)
            {
                firstPersonControls.currentHealth -= enemyBulletDamage;
            }

            Destroy(gameObject);    
        }
    }

}
