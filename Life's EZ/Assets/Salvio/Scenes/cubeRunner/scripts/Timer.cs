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

    private void Start()
    {
        Animation();
    }
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

    void Animation()
    {
        LeanTween.rotateZ(this.gameObject, 8, 0.8f).setLoopPingPong().setEaseInOutSine();
        LeanTween.scale(this.gameObject, new Vector2(1, 1), 0.8f).setLoopPingPong().setEaseInOutSine();
    }
}
