using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneManager : MonoBehaviour
{
    private List<GameObject> Clones;

    private void Start()
    {
        GameObject[] clonesArr = GameObject.FindGameObjectsWithTag("Player");
        Clones = new List<GameObject>(clonesArr);
    }

    public List<GameObject> GetClones()
    {
        return Clones;
    }

    public void RemoveClone(GameObject clone)
    {
        Clones.Remove(clone);
    }
}
