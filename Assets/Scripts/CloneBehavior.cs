using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneBehavior : MonoBehaviour
{
    public Weapon CurrentWeapon;

    private void Awake()
    {
        CurrentWeapon.SetClone(gameObject);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (CurrentWeapon.CanShoot())
            {
                CurrentWeapon.FireShot();
            }
        }
    }
}
