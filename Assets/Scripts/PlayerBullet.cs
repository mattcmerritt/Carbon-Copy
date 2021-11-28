using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    private Vector2 Direction;
    private float Speed;
    private float Damage;

    private Rigidbody2D rb;

    private void Start()
    {
        PlayerAim aim = FindObjectOfType<PlayerAim>();
        Direction = aim.GetDirection();
        if (Direction == Vector2.zero)
        {
            Direction = Vector2.right;
        }
        transform.eulerAngles = new Vector3(0f, 0f, aim.GetAngle());
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
        else if (collision.tag == "Enemy")
        {
            Destroy(gameObject);
        }
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
