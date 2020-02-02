using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public bool startTimer;
    public Text timerText;
    int seconds;
    int minutes;

    private void Update()
    {
        if (startTimer)
        {
            seconds += 1;
            if (seconds >= 60)
            {
                minutes+=1;
                seconds = 0;
            }
        }      

        timerText.text = timer(seconds, minutes);
    }

    public void ResetTimer()
    {
        seconds = 0;
        minutes = 0;
    }

    string timer(int seconds, int minutes)
    {
        string t = minutes + "." + seconds+" s";
        return t;
    }
}
