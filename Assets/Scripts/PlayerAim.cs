using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    private Vector2 MousePosition;
    private Vector2 MouseDirection;
    private float Angle;

    private int Selected;
    private GameObject SelectedClone;
    public CloneManager Clones;

    private void Start()
    {
        Clones = FindObjectOfType<CloneManager>();
        SelectedClone = Clones.GetClones()[Selected];

        MousePosition = Vector2.zero;
        MouseDirection = Vector2.zero;
    }

    private void Update()
    {
        MousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Clones.GetClones().Count != 0)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Selected++;
                Selected = Selected % Clones.GetClones().Count;
                SelectedClone = Clones.GetClones()[Selected];
            }

            // check that the selected clone hasn't changed
            if (Selected > Clones.GetClones().Count || SelectedClone != Clones.GetClones()[Selected])
            {
                Selected = Clones.GetClones().FindIndex(MatchesSelected);
                // selected clone is no longer present, select first
                if (Selected < 0)
                {
                    Selected = 0;
                }
            }

            MouseDirection = (MousePosition - Clones.GetClones()[Selected].GetComponent<Rigidbody2D>().position).normalized;

            Angle = -Mathf.Atan2(MousePosition.x - Clones.GetClones()[Selected].GetComponent<Rigidbody2D>().position.x, MousePosition.y - Clones.GetClones()[Selected].GetComponent<Rigidbody2D>().position.y) * Mathf.Rad2Deg + 90;
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

    private bool MatchesSelected(GameObject obj)
    {
        return obj == SelectedClone;
    }
}
