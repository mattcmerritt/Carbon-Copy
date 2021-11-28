using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneBehavior : MonoBehaviour
{
    private Weapon CurrentWeapon;
    private float Health = 100f;

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Unfriendly")
        {
            EnemyBullet bullet = collision.GetComponent<EnemyBullet>();
            Health -= bullet.GetDamage();
            Destroy(collision.gameObject);

            if (Health <= 0f)
            {
                // Player has died
                CloneManager manager = FindObjectOfType<CloneManager>();
                manager.RemoveClone(gameObject);
                Destroy(gameObject);
            }
        }
    }
}
