using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClonePod : MonoBehaviour
{
    private CloneManager Clones;

    private void Awake()
    {
        Clones = FindObjectOfType<CloneManager>();
    }

    public void Collect()
    {
        Clones.AddClone(transform.position);
    }

    public void RemoveFromRoom()
    {
        Room currentRoom = FindObjectOfType<Room>();
        currentRoom.RemoveCollected(gameObject);
    }

    public bool CanCollect()
    {
        return Clones.GetClones().Count < 4;
    }
}
