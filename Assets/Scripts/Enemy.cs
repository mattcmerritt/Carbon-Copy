using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    protected CloneManager Clones;
    protected GameObject Target;
    [SerializeField]
    protected float Health = 100f;
    private float MaxHealth;
    protected float Damage;

    // health bar
    public Image HealthBar;
    private RectTransform BarTransform;
    private float BeginAnchor, EndAnchor, CurrentAnchor;

    protected virtual void Awake()
    {
        Clones = FindObjectOfType<CloneManager>();

        BarTransform = HealthBar.GetComponent<RectTransform>();
        BeginAnchor = BarTransform.anchorMin.x;
        EndAnchor = BarTransform.anchorMax.x;
        CurrentAnchor = EndAnchor;

        MaxHealth = Health;
    }

    protected virtual void Update()
    {
        Target = FindNearestClone();
    }

    protected GameObject FindNearestClone()
    {
        float minDistance = float.MaxValue;
        GameObject selectedClone = null;
        for (int i = 0; i < Clones.GetClones().Count; i++)
        {
            float currentDistance = Vector3.Distance(Clones.GetClones()[i].transform.position, transform.position);
            if (currentDistance < minDistance)
            {
                minDistance = currentDistance;
                selectedClone = Clones.GetClones()[i];
            }
        }

        return selectedClone;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Friendly")
        {
            PlayerBullet bullet = collision.GetComponent<PlayerBullet>();

            Health -= bullet.GetDamage();

            CurrentAnchor = (EndAnchor - BeginAnchor) * (Health / MaxHealth) + BeginAnchor;
            BarTransform.anchorMax = new Vector2(CurrentAnchor, BarTransform.anchorMax.y);

            if (Health <= 0f)
            {
                Destroy(gameObject);
            }

            Destroy(collision.gameObject);
        }
    }
}
