using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private Vector2 Direction;
    private float Speed;
    private float Damage;

    private Rigidbody2D rb;

    private void Start()
    {
        // get direction vector from turret
        // get rotation angle from turret
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + Direction * Time.fixedDeltaTime * Speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Walls")
        {
            Destroy(gameObject);
        }
    }

    public void SetProperties(float angle, Vector2 direction)
    {
        Direction = direction;
        if (Direction == Vector2.zero)
        {
            Direction = Vector2.right;
        }
        transform.eulerAngles = new Vector3(0f, 0f, angle);
    }

    public void SetStats(float damage, float speed)
    {
        Speed = speed;
        Damage = damage;
    }

    public float GetDamage()
    {
        return Damage;
    }
}
