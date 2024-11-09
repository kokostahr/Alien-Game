using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalExit : MonoBehaviour
{
    [Header("PORTAL SETTINGS")]
    [Space(5)]
    public GameObject portal;
    public string portalExit;
    public AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
       if (other.CompareTag("Player"))
       {
        SceneManager.LoadScene(portalExit);
       }
    }
}
