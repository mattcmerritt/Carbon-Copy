using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    private CloneManager Clones;
    public int Health;

    private void Awake()
    {
        Clones = FindObjectOfType<CloneManager>();
    }

    public void Collect()
    {
        List<GameObject> clonesList = Clones.GetClones();
        foreach (GameObject obj in clonesList)
        {
            CloneBehavior behavior = obj.GetComponent<CloneBehavior>();
            behavior.AddHealth(Health);
        }
    }

    public void RemoveFromRoom()
    {
        Room currentRoom = FindObjectOfType<Room>();
        currentRoom.RemoveCollected(gameObject);
    }
}
