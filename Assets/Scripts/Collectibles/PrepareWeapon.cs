using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrepareWeapon : MonoBehaviour
{
    public float FireDelay;
    public float ShotDamage;
    public float MagSize;

    public Weapon WeaponToPrepare;

    private void Awake()
    {
        WeaponToPrepare.SetStats(FireDelay, ShotDamage, MagSize);
    }
}
