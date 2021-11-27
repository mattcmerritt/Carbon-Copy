using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneMovement : MonoBehaviour
{
    private Vector2 InputDirection;
    private Rigidbody2D rb;
    public float MoveSpeed;
    private Animator anim;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");
        InputDirection = new Vector2(inputX, inputY);
        InputDirection.Normalize();

        anim.SetFloat("Horizontal", inputX);
        anim.SetFloat("Vertical", inputY);
        anim.SetFloat("Movement", InputDirection.magnitude);
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + InputDirection * MoveSpeed * Time.fixedDeltaTime);
    }
}
