using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameUI : MonoBehaviour
{
    public GameObject WinPopup, LosePopup;
    public TMP_Text WinText, LoseText;

    public void Awake()
    {
        WinPopup.SetActive(false);
        LosePopup.SetActive(false);
    }

    public void ShowWin()
    {
        WinPopup.SetActive(true);
        LosePopup.SetActive(false);

        // get time
        TimerUI tUI = FindObjectOfType<TimerUI>();
        tUI.StopTimer();
        float time = tUI.SaveTime();

        if (PlayerPrefs.HasKey("highscore"))
        {
            float highscore = PlayerPrefs.GetFloat("highscore");
            if (highscore <= time)
            {
                WinText.SetText("Time: " + TimerUI.FormatTime(time));
            }
            else
            {
                WinText.SetText("Time: " + TimerUI.FormatTime(time) + "\nNew Personal Best!");
                PlayerPrefs.SetFloat("highscore", time);
            }
        }
        else
        {
            WinText.SetText("Time: " + TimerUI.FormatTime(time) + "\nNew Personal Best!");
            PlayerPrefs.SetFloat("highscore", time);
        }
        PlayerPrefs.Save();
        
    }

    public void ShowLoss()
    {
        WinPopup.SetActive(false);
        LosePopup.SetActive(true);

        // get rooms cleared
        RoomCountUI rcUI = FindObjectOfType<RoomCountUI>();
        int rooms = rcUI.GetRoomsCleared();

        TimerUI tUI = FindObjectOfType<TimerUI>();
        tUI.StopTimer();

        LoseText.SetText("Rooms Cleared: " + rooms);
    }
}
