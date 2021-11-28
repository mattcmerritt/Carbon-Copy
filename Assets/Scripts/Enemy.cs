using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected CloneManager Clones;
    protected GameObject Target;
    protected float Health;
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

    public void TakeDamage(float damage)
    {
        Health -= Damage;
        if (Health <= 0f)
        {
            Destroy(gameObject);
        }
    }
}
