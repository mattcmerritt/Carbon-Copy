using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : Enemy
{
    private float Angle;
    [SerializeField]
    private Vector2 ShotDirection;
    private float[] AngleBorders = { 22.5f, 67.5f, 112.5f, 157.5f, 202.5f, 247.5f, 292.5f, 337.5f };
    public Sprite[] TurretSprites;
    private SpriteRenderer Renderer;

    // weapon details
    public float FireDelay;
    public float ShotDamage;
    private float ShotSpeed = 10f;

    private bool CooldownActive;
    private float CurrentDelay;

    public GameObject BulletPrefab;

    protected override void Awake()
    {
        // basic enemy behavior first
        base.Awake();

        Renderer = GetComponent<SpriteRenderer>();
    }

    protected override void Update()
    {
        // basic enemy behavior first
        base.Update();

        // turret behavior, only if a target can be found
        if (Target != null)
        {
            // find angle to target
            Angle = (360 - Mathf.Atan2(transform.position.x - Target.transform.position.x, transform.position.y - Target.transform.position.y) * Mathf.Rad2Deg - 90) % 360;

            // choosing the correctly angled sprite
            if (Angle <= AngleBorders[0] || Angle > AngleBorders[AngleBorders.Length - 1])
            {
                Renderer.sprite = TurretSprites[0];
            }
            for (int i = 1; i < AngleBorders.Length; i++)
            {
                float maxBound = AngleBorders[i];
                float minBound = AngleBorders[i - 1];

                if (Angle > minBound && Angle <= maxBound)
                {
                    Renderer.sprite = TurretSprites[i];
                }
            }

            // calculate the shot direction
            ShotDirection = new Vector2(Target.transform.position.x - transform.position.x, Target.transform.position.y - transform.position.y);
            ShotDirection.Normalize();

            // weapon cooldown updates
            if (CooldownActive)
            {
                CurrentDelay += Time.deltaTime;
            }
            if (CurrentDelay > FireDelay)
            {
                CooldownActive = false;
            }

            // shooting
            if (CanShoot())
            {
                FireShot();
            }
        }
    }

    private bool CanShoot()
    {
        return !CooldownActive;
    }

    private void FireShot()
    {
        CurrentDelay = 0f;
        CooldownActive = true;

        GameObject bullet = Instantiate(BulletPrefab, transform.position, Quaternion.identity);
        EnemyBullet bulletScript = bullet.GetComponent<EnemyBullet>();
        bulletScript.SetProperties(Angle, ShotDirection);
        bulletScript.SetStats(ShotDamage, ShotSpeed);
    }
}
