using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUI : MonoBehaviour
{
    private CloneManager Clones;
    public GameObject[] CloneUIs;
    public Image[] WeaponSpots;

    private void Awake()
    {
        Clones = FindObjectOfType<CloneManager>();

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
            Weapon held = cloneBehavior.GetWeapon();
            if (held == null)
            {
                WeaponSpots[cloneBehavior.CloneIndex].color = new Color(1, 1, 1, 0);
                WeaponSpots[cloneBehavior.CloneIndex].sprite = null;
            }
            else
            {
                SpriteRenderer weaponSpriteRenderer = held.GetComponent<SpriteRenderer>();
                Sprite weaponSprite = weaponSpriteRenderer.sprite;
                WeaponSpots[cloneBehavior.CloneIndex].color = new Color(1, 1, 1, 1);
                WeaponSpots[cloneBehavior.CloneIndex].sprite = weaponSprite;
            }
        }
    }
}
