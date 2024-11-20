using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalSound : MonoBehaviour
{
    public AudioSource portalSOUNDING;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            portalSOUNDING.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            portalSOUNDING.Stop();
        }
    }
}
