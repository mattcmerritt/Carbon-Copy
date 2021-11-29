using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUI : MonoBehaviour
{
    private CloneManager Clones;
    public GameObject[] CloneUIs;
    public RectTransform[] HealthBars;
    private float[] MinAnchors;
    private float[] MaxAnchors;

    private void Awake()
    {
        Clones = FindObjectOfType<CloneManager>();

        MinAnchors = new float[HealthBars.Length];
        MaxAnchors = new float[HealthBars.Length];

        for (int i = 0; i < HealthBars.Length; i++)
        {
            MinAnchors[i] = HealthBars[i].anchorMin.x;
            MaxAnchors[i] = HealthBars[i].anchorMax.x;
        } 

        foreach (GameObject cloneUI in CloneUIs)
        {
            cloneUI.SetActive(false);
        }
    }

    private void Update()
    {
        foreach (GameObject cloneUI in CloneUIs)
        {
            cloneUI.SetActive(false);
        }
        List<GameObject> cloneList = Clones.GetClones();
        foreach (GameObject clone in cloneList)
        {
            CloneBehavior cloneBehavior = clone.GetComponent<CloneBehavior>();

            // show UI for the selected clone
            CloneUIs[cloneBehavior.CloneIndex].SetActive(true);

            // adjust bar based on health
            float currentHealthAnchor = (MaxAnchors[cloneBehavior.CloneIndex] - MinAnchors[cloneBehavior.CloneIndex]) * cloneBehavior.GetHealthPercent() + MinAnchors[cloneBehavior.CloneIndex];
            HealthBars[cloneBehavior.CloneIndex].anchorMax = new Vector2(currentHealthAnchor, HealthBars[cloneBehavior.CloneIndex].anchorMax.y);
        }
    }
}
