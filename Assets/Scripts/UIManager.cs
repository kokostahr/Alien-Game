using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Video;

public class UIManager : MonoBehaviour
{

    [Header("MAINMENU UI SETUP")]
    [Space(5)]
    public Camera mainCamera; // Reference to the camera (assign in the Inspector)
    public float rotationSpeed = 1f; // Speed at which the camera rotates
    private bool isRotating = false; // Track if the camera is currently rotating
    public GameObject[] UIElements;
    public GameObject initialButton;
    public VideoPlayer mainMenuPlayer;
    public GameObject screenPanel;


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
        mainMenuPlayer.Play();

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
        screenPanel.SetActive(false);

        foreach (GameObject UIelement in UIElements)
        {
            UIelement.SetActive(true);
        }
    }
}
