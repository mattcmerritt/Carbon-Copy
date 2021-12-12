using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private PlayerAim Aim;
    private Room CurrentRoom;

    private Vector3 PreviousPosition;

    public float Speed;

    private void Awake()
    {
        Aim = FindObjectOfType<PlayerAim>();
    }

    private void LateUpdate()
    {
        CurrentRoom = FindObjectOfType<Room>();
        if (CurrentRoom.IsLarge)
        {
            Vector3 currentPosition = Aim.transform.position;
            Vector3 newPosition = Vector3.Lerp(PreviousPosition, currentPosition, Speed);
            transform.position = new Vector3(
                Mathf.Clamp(newPosition.x, CurrentRoom.CameraBoundLeft, CurrentRoom.CameraBoundRight),
                Mathf.Clamp(newPosition.y, CurrentRoom.CameraBoundBottom, CurrentRoom.CameraBoundTop),
                -10);
            PreviousPosition = currentPosition;
        }
        else
        {
            transform.position = Vector3.zero;
            transform.position = new Vector3(transform.position.x, transform.position.y, -10);
        }
    }
}
