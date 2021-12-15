using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RoomCountUI : MonoBehaviour
{
    private RoomManager RM;
    public TMP_Text Text;

    private void Awake()
    {
        RM = FindObjectOfType<RoomManager>();
    }

    private void Update()
    {
        Text.SetText("Remaining Rooms: " + (RM.TotalRooms - RM.RoomsCleared));
    }
}
