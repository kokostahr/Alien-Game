using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleportation : MonoBehaviour
{

    //https://youtu.be/xmhm5jGwonc?si=wIwR1lQ7rXgpZsfj using this video as a guideline to teleport the player without using ridigbody
    //public GameObject player;
    public GameObject teleportPosition;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Teleporter"))
        {
            gameObject.transform.position = teleportPosition.transform.position;
            Debug.Log("Teleporting");
        }
    }

    //void Teleportat(Collider other)
    //{
    //    if (other.CompareTag("Teleporter"))
    //    {
    //        gameObject.transform.position = teleportPosition.transform.position;
    //        Debug.Log("Teleporting");
    //    }
    //}
}
