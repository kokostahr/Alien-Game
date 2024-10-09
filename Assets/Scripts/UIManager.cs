using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{

    [Header("MAINMENU UI SETUP")]
    [Space(5)]
    public Camera mainCamera; // Reference to the camera (assign in the Inspector)
    public float rotationSpeed = 1f; // Speed at which the camera rotates
    private bool isRotating = false; // Track if the camera is currently rotating
    public GameObject[] UIElements;
    public GameObject initialButton;

    [Header("COUNTDOWN TIMER SETTNGS")]
    [Space(5)]
    //UI SETUP FOR THE COUNTDOWN
    public string  levelToLoad; //A string so we can put the name of the scene we want to load. 
    public TextMeshProUGUI countDownTimer;  //Text that will display the countdown timer
    public float timeRemaining = 300f; //Amount of time remaining in game, in seconds. 

    private void Update()
    {
        timeRemaining -= Time.deltaTime;
        countDownTimer.text = "Time Remaining = " + timeRemaining.ToString();
        if (timeRemaining <= 0)
        {
            SceneManager.LoadScene(levelToLoad);
        }
    }

    // Method to rotate the camera left by 90 degrees
    public void RotateCameraLeftBy90Degrees()
    {
        if (!isRotating) // Prevent triggering multiple rotations simultaneously
        {
            StartCoroutine(RotateCameraCoroutine(15f));
        }
    }

    public void LoadScene(string sceneName) //To load the relevant scene when you click the button.
    {
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame() //To exit the apllication when you click the button
    {
        Application.Quit();
    }

    // Coroutine to smoothly rotate the camera
    private IEnumerator RotateCameraCoroutine(float angle)
    {
        isRotating = true;
        Quaternion startRotation = mainCamera.transform.rotation; // Initial rotation
        Quaternion endRotation = startRotation * Quaternion.Euler(0, -angle, 0); //Target rotation

    float rotationProgress = 0f;

        while (rotationProgress < 1f)
        {
            rotationProgress += Time.deltaTime * (rotationSpeed / angle); //Normalize the rotation speed based on angle

        mainCamera.transform.rotation = Quaternion.Lerp(startRotation, endRotation, rotationProgress); // Smoothly interpolate rotation
            yield return null;
        }

        mainCamera.transform.rotation = endRotation; // Ensure exact final rotation
        isRotating = false;
        initialButton.SetActive(false);

        foreach (GameObject UIelement in UIElements)
        {
            UIelement.SetActive(true);
        }
    }
}
