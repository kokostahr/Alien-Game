using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    //a script to make audio move seemlessly between scenes
    [Header("AUDIO SOURCE SETUP")]
    [Space(5)]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource sfxSource;
    [SerializeField] AudioSource weaponSource;
    [SerializeField] AudioSource footstepSource;

    [Header("AUDIO CLIP SETUP")]
    [Space(5)]
    //Calling these variables so we can store them.
    public AudioClip bgMusic;
    public AudioClip herScreamSFX;
    public AudioClip gunShot;
    public AudioClip enemyCaveSound;
    public AudioClip howLongHave;
    public AudioClip walkingInside;
    public AudioClip walkingOutside;
    public AudioClip waterSlosh;
    //public AudioClip portalSound;
    public AudioClip hasToGetOut;


    private static AudioManager instance; //instance to make sure we can detect the current gameobj and delete unnecesary clones
    
    //SettingUp for the other 'audio' clips
    //

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        musicSource.clip = bgMusic;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);

    }

    public void WeaponSFX(AudioClip clip)
    {
        weaponSource.PlayOneShot(clip);
    }

    public void FootstepSFX(AudioClip clip)
    {
        footstepSource.PlayOneShot(clip);
    }

    void Update()
    {

    }
}
