using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    public Camera mainCamera; // Reference to the camera (assign in the Inspector)
    public float rotationSpeed = 45f; // Speed at which the camera rotates
    private bool isRotating = false; // Track if the camera is currently rotating
    public GameObject[] UIElements; //Other buttons and UI components that we want to appear will be here.
    public GameObject initialButton; //The begin button

    // Method to rotate the camera left by 90 degrees
    public void RotateCameraLeftBy90Degrees()
    {
        if (!isRotating) // Prevent triggering multiple rotations simultaneously
        {
            StartCoroutine(RotateCameraCoroutine(90f));
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
        Quaternion startRotation = mainCamera.transform.rotation; // Initialrotation
        Quaternion endRotation = startRotation * Quaternion.Euler(0, -angle, 0); //Target rotation

    float rotationProgress = 0f;

        while (rotationProgress < 1f)
        {
            rotationProgress += Time.deltaTime * (rotationSpeed / angle); //Normalize the rotation speed based on angle

        mainCamera.transform.rotation = Quaternion.Lerp(startRotation,
        endRotation, rotationProgress); // Smoothly interpolate rotation
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
