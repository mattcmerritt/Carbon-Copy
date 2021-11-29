using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private float FireDelay;
    private float ShotDamage;
    private float MagSize;
    private float CurrentAmmo;
    private float ShotSpeed = 10f;

    [SerializeField]
    private bool CooldownActive;
    [SerializeField]
    private float CurrentDelay;

    private GameObject Holder;
    public GameObject BulletPrefab;

    public void SetStats(float fireDelay, float shotDamage, float magSize)
    {
        FireDelay = fireDelay;
        ShotDamage = shotDamage;
        MagSize = magSize;
        CurrentAmmo = magSize;
    }

    private void Update()
    {
        if (CooldownActive)
        {
            CurrentDelay += Time.deltaTime;
        }
        if (CurrentDelay > FireDelay)
        {
            CooldownActive = false;
        }
    }

    public bool CanShoot()
    {
        return !CooldownActive;
    }

    public void FireShot()
    {
        CurrentDelay = 0f;
        CooldownActive = true;

        GameObject bullet = Instantiate(BulletPrefab, Holder.transform.position, Quaternion.identity);
        PlayerBullet bulletScript = bullet.GetComponent<PlayerBullet>();
        bulletScript.SetStats(ShotDamage, ShotSpeed);
    }

    public void SetClone(GameObject clone)
    {
        Holder = clone;
    }

    public void Drop()
    {
        transform.SetParent(null);
        Collider2D collider = GetComponent<Collider2D>();
        collider.enabled = true;
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        renderer.enabled = true;
        Collectible collectible = GetComponent<Collectible>();
        collectible.enabled = true;
    }
}
