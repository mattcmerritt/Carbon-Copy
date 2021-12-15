using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> Clones;

    public GameObject[] ClonePrefabs;

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

    public void AddClone(Vector3 location)
    {
        int currentIndex = 0;
        bool hasBeenPlaced = false;
        while (!hasBeenPlaced && currentIndex < ClonePrefabs.Length)
        {
            // find if this color clone is already loaded into the game
            bool alreadyExists = false;
            foreach (GameObject clone in Clones)
            {
                CloneBehavior behavior = clone.GetComponent<CloneBehavior>();
                if (behavior != null && behavior.CloneIndex == currentIndex)
                {
                    alreadyExists = true;
                }
            }

            if (!alreadyExists)
            {
                // make clone
                GameObject newClone = Instantiate(ClonePrefabs[currentIndex], location, Quaternion.identity);
                Clones.Add(newClone);
                hasBeenPlaced = true;
            }
            else
            {
                currentIndex++;
            }

        }
    }
}
