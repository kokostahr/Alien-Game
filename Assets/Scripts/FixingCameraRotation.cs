using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixingCameraRotation : MonoBehaviour
{
    //public Transform player; // Drag the player here
    //private Vector3 initialOffset;
    //private Quaternion initialRotation;

    public Transform player; // Assign player in Inspector
    public Vector3 offset;   // Offset to position the camera where it needs to be

    void Start()
    {
        // Get initial offset if the camera is where it should be
        offset = transform.position - player.position;
    }

    void LateUpdate()
    {
        // Only follow position; no parenting involved
        transform.position = player.position + offset;
    }
}
