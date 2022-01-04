using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeCounter : MonoBehaviour
{
    public Text time;
    [ReadOnlyInspecter] public bool TimerEnd = true;
    bool countUp = true;
    float targetTime = 0f;
    float currentTime = 0f;

    public void RunTimer(bool countup, float targettime)
    {
        TimerEnd = false;
        countUp = countup;
        targetTime = targettime;
        currentTime = countUp ? 0f : targetTime;
    }

    void Update()
    {
        if (!TimerEnd)
        {
            if (countUp)
            {
                currentTime += Time.deltaTime;
                if (currentTime >= targetTime)
                {
                    currentTime = targetTime;
                    TimerEnd = true;
                }
            }
            else
            {
                currentTime -= Time.deltaTime;
                if (currentTime <= 0f)
                {
                    currentTime = 0f;
                    TimerEnd = true;
                }
            }
            time.text = currentTime.ToString("0.0");
        }
    }
}
