using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private PlayerAim Aim;

    private void Awake()
    {
        Aim = FindObjectOfType<PlayerAim>();
    }

    private void Update()
    {
        transform.eulerAngles = new Vector3(0f, 0f, Aim.GetAngle());
    }
}
