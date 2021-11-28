using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneBehavior : MonoBehaviour
{
    private Weapon CurrentWeapon;

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (CurrentWeapon != null)
            {
                if (CurrentWeapon.CanShoot())
                {
                    CurrentWeapon.FireShot();
                }
            }    
        }
    }

   public void Collect(GameObject collectible)
    {
        Weapon weapon = collectible.GetComponent<Weapon>();

        if (weapon != null)
        {
            weapon.SetClone(gameObject);
            CurrentWeapon = weapon;
        }
    }
}
