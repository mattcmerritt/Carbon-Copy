using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    public GameObject WinPopup, LosePopup;

    public void Awake()
    {
        WinPopup.SetActive(false);
        LosePopup.SetActive(false);
    }

    public void ShowWin()
    {
        WinPopup.SetActive(true);
        LosePopup.SetActive(false);
    }

    public void ShowLoss()
    {
        WinPopup.SetActive(false);
        LosePopup.SetActive(true);
    }
}
