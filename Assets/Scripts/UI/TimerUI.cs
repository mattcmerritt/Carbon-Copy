using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerUI : MonoBehaviour
{
    public float TimeElapsed;
    public TMP_Text Text;

    private bool Running = true;

    private void Update()
    {
        if (Running)
        {
            TimeElapsed += Time.deltaTime;
            UpdateTimer();
        }
    }

    private void UpdateTimer()
    {
        int minutes = Mathf.FloorToInt(TimeElapsed / 60);
        int seconds = Mathf.FloorToInt(TimeElapsed % 60);
        int remaining = Mathf.FloorToInt(TimeElapsed % 60 % 1 * 1000);

        Text.SetText("" + minutes.ToString("D2") + ":" + seconds.ToString("D2") +  "." + remaining.ToString("D3"));
    }

    public float SaveTime()
    {
        return TimeElapsed;
    }

    public static string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        int remaining = Mathf.FloorToInt(time % 60 % 1 * 1000);

        return "" + minutes.ToString("D2") + ":" + seconds.ToString("D2") + "." + remaining.ToString("D3");
    }

    public void StopTimer()
    {
        Running = false;
    }

}
