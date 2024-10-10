using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CountDownTimer : MonoBehaviour
{

    [Header("COUNTDOWN TIMER SETTNGS")]
    [Space(5)]
    //UI SETUP FOR THE COUNTDOWN
    public string levelToLoad; //A string so we can put the name of the scene we want to load. 
    public TextMeshProUGUI countDownTimer;  //Text that will display the countdown timer
    public float timeRemaining = 300f; //Amount of time remaining in game, in seconds. 

    private void Update()
    {
        timeRemaining -= Time.deltaTime;
        //Dividing the time into mins and seconds
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
        //Setting the timer into mintues and seconds format
        countDownTimer.text = "Time Remaining = " + string.Format("{0:00}:{1:00}", minutes, seconds);

        //CHANGING THE COLOUR OF THE TEXT AT THE LAST MINUTE
        if (timeRemaining <= 60)
        {
            countDownTimer.color = Color.red;
        }

        if (timeRemaining <= 0)
        {
            SceneManager.LoadScene(levelToLoad);
        }
    }
}
