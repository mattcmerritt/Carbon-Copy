using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected List<GameObject> Clones;
    protected GameObject Target;
    protected float Health;
    protected float Damage;

    protected virtual void Awake()
    {
        GameObject[] clones = GameObject.FindGameObjectsWithTag("Player");
        Clones = new List<GameObject>(clones);
    }

    protected virtual void Update()
    {
        Target = FindNearestClone();
    }

    protected GameObject FindNearestClone()
    {
        float minDistance = float.MaxValue;
        GameObject selectedClone = null;
        for (int i = 0; i < Clones.Count; i++)
        {
            float currentDistance = Vector3.Distance(Clones[i].transform.position, transform.position);
            if (currentDistance < minDistance)
            {
                minDistance = currentDistance;
                selectedClone = Clones[i];
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
