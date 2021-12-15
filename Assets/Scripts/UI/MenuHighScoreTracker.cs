using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuHighScoreTracker : MonoBehaviour
{
    public TMP_Text Label;

    private void Awake()
    {
        if (PlayerPrefs.HasKey("highscore"))
        {
            Label.SetText("Personal Best Time: " + TimerUI.FormatTime(PlayerPrefs.GetFloat("highscore")));
        }
    }
}
