using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneMovement : MonoBehaviour
{
    private Vector2 InputDirection;
    private Rigidbody2D rb;
    public float MoveSpeed;
    private Animator anim;

    // variables for initially loading rooms
    private bool IsLoadingRoom;
    private Vector2 LoadedPosition;

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
        if (!IsLoadingRoom)
        {
            rb.MovePosition(rb.position + InputDirection * MoveSpeed * Time.fixedDeltaTime);
        }
        else
        {
            //rb.MovePosition(LoadedPosition);
            transform.position = LoadedPosition;
            IsLoadingRoom = false;

            // reenable collider after moving
            Collider2D collider = GetComponent<Collider2D>();
            collider.enabled = true;
        }
    }

    public void MoveToPosition(Vector2 position)
    {
        // disable collider temporarily to prevent collisions
        Collider2D collider = GetComponent<Collider2D>();
        collider.enabled = false;

        LoadedPosition = position;
        IsLoadingRoom = true;
    }
}
