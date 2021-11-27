using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    private Vector2 MousePosition;
    private Vector2 MouseDirection;
    private float Angle;

    private int Selected;
    public List<Rigidbody2D> Clones;

    private void Start()
    {
        Clones = new List<Rigidbody2D>();
        Rigidbody2D[] rbs = FindObjectsOfType<Rigidbody2D>();
        foreach (Rigidbody2D rb in rbs)
        {
            if (rb.tag == "Player")
            {
                Clones.Add(rb);
            }
        }

        MousePosition = Vector2.zero;
        MouseDirection = Vector2.zero;
    }

    private void Update()
    {
        MousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        MouseDirection = (MousePosition - Clones[Selected].position).normalized;

        Angle = -Mathf.Atan2(MousePosition.x - Clones[Selected].position.x, MousePosition.y - Clones[Selected].position.y) * Mathf.Rad2Deg + 90;

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Selected++;
            Selected = Selected % Clones.Count;
        }
    }

    public Vector2 GetDirection()
    {
        return MouseDirection;
    }

    public float GetAngle()
    {
        return Angle;
    }
}
