using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    Vector3 enemyMove = Vector3.left;
    public float enemySpeed;

    private bool movingRight = true;

    void Start()
    {
        
    }


    void Update()
    {
        if (movingRight)
        {
            transform.Translate(Vector3.up * enemySpeed * Time.deltaTime);

            if (transform.position.x <= -1)
            {
                movingRight = false;
            }
        }
        else 
        {
            transform.Translate(Vector3.down * enemySpeed * Time.deltaTime);
            if (transform.position.x >= 1)
            {
                movingRight = true;
            }
        }
        
    }
}

