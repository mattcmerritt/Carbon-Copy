using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneBehavior : MonoBehaviour
{
    private Weapon CurrentWeapon;
    private float Health = 100f;
    private float MaxHealth;

    public int CloneIndex;

    private float WeaponSwapCooldown;

    private void Awake()
    {
        MaxHealth = Health;
    }

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
        WeaponSwapCooldown += Time.deltaTime;
    }

   public void Collect(GameObject collectible)
   {
        Weapon weapon = collectible.GetComponent<Weapon>();

        if (weapon != null && CurrentWeapon == null)
        {
            weapon.SetClone(gameObject);
            CurrentWeapon = weapon;
        }
        else if (weapon != null && CurrentWeapon != null)
        {
            CurrentWeapon.Drop();
            weapon.SetClone(gameObject);
            CurrentWeapon = weapon;
        }

        // reset weapon cooldown
        WeaponSwapCooldown = 0f;
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

                // drop weapon
                if (CurrentWeapon != null)
                {
                    CurrentWeapon.Drop();
                    CurrentWeapon = null;
                }

                manager.RemoveClone(gameObject);
                Destroy(gameObject);
            }
        }

        if (collision.tag == "Exits")
        {
            RoomManager roomManager = FindObjectOfType<RoomManager>();
            // used one of the side exits
            if (Mathf.Abs(transform.position.x) > Mathf.Abs(transform.position.y))
            {
                if (transform.position.x > 0)
                {
                    roomManager.MoveRoom(RoomManager.RIGHT);
                }
                else
                {
                    roomManager.MoveRoom(RoomManager.LEFT);
                }
            }
            // used the top or bottom exit
            else
            {
                if (transform.position.y > 0)
                {
                    roomManager.MoveRoom(RoomManager.UP);
                }
                else
                {
                    roomManager.MoveRoom(RoomManager.DOWN);
                }
            }
        }
    }

    public float GetHealthPercent()
    {
        return Health / MaxHealth;
    }

    public bool HasWeapon()
    {
        return CurrentWeapon != null;
    }

    public bool CanGrabWeapon()
    {
        return WeaponSwapCooldown > 0.5f;
    }
}
