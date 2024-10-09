using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDoor : MonoBehaviour
{

    [Header("TRAP DOOR SETTINGS")]
    [Space(5)]
    public GameObject trappedDoor;
    public GameObject trapDoorUI;


    public void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Key"))
        {
            trappedDoor.SetActive(true);
        }

        trapDoorUI.SetActive(true);
    }




}
