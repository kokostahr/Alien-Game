using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    Vector3 enemyMove = Vector3.left;
    public float enemySpeed;

    void Start()
    {
        
    }


    void Update()
    {
        this.transform.Translate(enemyMove * enemySpeed * Time.deltaTime);

        if (this.transform.position.x <= -0.176f)
        {
            enemyMove = Vector3.right;
        }
        else if (this.transform.position.x >= 0.176f)
        {
            enemyMove = Vector3.left;


        }
    }
}
