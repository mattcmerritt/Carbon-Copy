using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected CloneManager Clones;
    protected GameObject Target;
    [SerializeField]
    protected float Health = 100f;
    protected float Damage;

    protected virtual void Awake()
    {
        Clones = FindObjectOfType<CloneManager>();
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
            if (Health <= 0f)
            {
                Destroy(gameObject);
            }

            Destroy(collision.gameObject);
        }
    }
}
