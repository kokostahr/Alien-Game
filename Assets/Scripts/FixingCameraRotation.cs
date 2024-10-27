using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedCameraPosition : MonoBehaviour
{
    public Transform player; // Drag your player here
    private Vector3 initialOffset;
    private Quaternion initialRotation;

    void Start()
    {
        // Set the initial position and rotation
        //transform.position = new Vector3(-0.61f, -0.07f, 1.09f); 
        transform.rotation = Quaternion.Euler(0, 0, 0);

        // Calculate the initial offset and rotation
        initialOffset = transform.position - player.position;
        initialRotation = transform.rotation;
    }

    void LateUpdate()
    {
        // Maintain the original offset and rotation
        transform.position = player.position + initialOffset;
        transform.rotation = initialRotation;
    }
}
