using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienController : MonoBehaviour
{
    //Script to make the alien move differently!
    //Reference the countdown timer script, then use time remaining and anim parametre (float)
    //to set different values for the different alien states, and say when 

    [Header("AUDIO MANAGEMENT")]
    [Space(5)]
    public AudioSource alienSounds;


    //Only need a trigger to detect when the player is within the playing field, then the alien sounds will go off

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            //audioManager.PlaySFX(audioManager.alienSounds);
            alienSounds.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            alienSounds.Stop();
        }
    }



}
