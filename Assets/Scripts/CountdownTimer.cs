using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{
    public static float currentTime = 0f;
    public static float time = 8.5f; // Time between black spawns.
    static float originalTime = 8.5f;
    float startTime;
    public static bool timerRunning = false; //as in, is the timercurrentlyrunning and not paused? I think?
    public static bool timerPaused = true;
    bool firstBeep = false;
    bool secondBeep = false;
    bool thirdBeep = false;
    public static bool chaosMode = false;


    Image countdownRing;

    void Start()
    {
        timerPaused = true;
        startTime = time;
        currentTime = startTime;
        countdownRing = GetComponent<Image>();
    }


    
    void Update()
    {
        if (!timerPaused)
        {
            currentTime -= 1 * Time.deltaTime;
            countdownRing.fillAmount = 1 - currentTime / time;
            if (currentTime <= 0)
            {
                currentTime = 0;
                timerRunning = false;
            }
            if (currentTime > 3 && firstBeep) //reset beeps if timer has restarted
            {
                firstBeep = false;
                secondBeep = false;
                thirdBeep = false;
            }
            else if(chaosMode && currentTime > 2)
            {
                secondBeep = false;
                thirdBeep = false;
            }
            if (currentTime <= 3 && !firstBeep)
            {
                if (!chaosMode)
                {
                    FindObjectOfType<AudioManager>().Play("beep");
                }
                firstBeep = true;
            }
            if (currentTime <= 2 && !secondBeep)
            {
                FindObjectOfType<AudioManager>().Play("beep");
                secondBeep = true;
            }
            if (currentTime <= 1 && !thirdBeep)
            {
                FindObjectOfType<AudioManager>().Play("beep");
                thirdBeep = true;
            }
        }
    }
    public void BreakTimer()
    {
        timerPaused = true;
    }

    public void FixTimer()
    {
        timerPaused = false;
    }
    public void StartTimer(float time)
    {
        firstBeep = false;
        secondBeep = false;
        thirdBeep = false;
        currentTime = time;
        timerPaused = false;
        timerRunning = true;
    }

    public void Reset()
    {
        time = originalTime;
        startTime = time;
        timerRunning = false;
    }
}
