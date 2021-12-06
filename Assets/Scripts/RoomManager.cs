using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    private Room LoadedRoom;

    private CloneManager Clones;

    public const int DOWN = 0, UP = 1, LEFT = 2, RIGHT = 3;

    public GameObject[][] RoomTemplates;
    public GameObject[][] RoomObjects;

    public GameObject StartRoom, Room1, Room2;

    public int CurrentRoomX, CurrentRoomY;
    public int PreviousDirection;

    private bool LoadingRoom;

    private void Awake()
    {
        Clones = FindObjectOfType<CloneManager>();

        // array is currently upside-down
        RoomTemplates = new GameObject[][] { new GameObject[] {StartRoom, Room1}, new GameObject[] {Room2, Room1} };
        RoomObjects = new GameObject[RoomTemplates.Length][];
        
        for (int row = 0; row < RoomObjects.Length; row++)
        {
            RoomObjects[row] = new GameObject[RoomTemplates[0].Length];
        }

        InitialRoomLoad();

        LoadCurrentRoom();
    }

    private void InitialRoomLoad()
    {
        for (int y = 0; y < RoomTemplates.Length; y++)
        {
            for (int x = 0; x < RoomTemplates[y].Length; x++)
            {
                RoomObjects[y][x] = Instantiate(RoomTemplates[y][x], Vector3.zero, Quaternion.identity);
                Room room = RoomObjects[y][x].GetComponent<Room>();
                room.InitialLoadLevel();
                room.UnloadLevel();
                room.gameObject.SetActive(false);
            }
        }
    }

    private void LoadCurrentRoom()
    {
        GameObject room = RoomObjects[CurrentRoomY][CurrentRoomX];
        room.SetActive(true);
        LoadedRoom = room.GetComponent<Room>();
        LoadedRoom.LoadLevel(Clones.GetClones().ToArray(), PreviousDirection);

        LoadingRoom = false;
    }

    private void UnloadPrevious(int x, int y)
    {
        GameObject prev = RoomObjects[y][x];
        Room prevRoom = prev.GetComponent<Room>();
        prevRoom.UnloadLevel();
        prev.SetActive(false);
    }

    public void MoveRoom(int direction)
    {
        Debug.Log("Loading " + LoadingRoom);
        if (!LoadingRoom)
        {
            int prevX = CurrentRoomX, prevY = CurrentRoomY;

            Debug.Log(direction);
            if (direction == DOWN)
            {
                CurrentRoomY--;
                PreviousDirection = UP;
            }
            else if (direction == UP)
            {
                CurrentRoomY++;
                PreviousDirection = DOWN;
            }
            else if (direction == LEFT)
            {
                CurrentRoomX--;
                PreviousDirection = RIGHT;
            }
            else if (direction == RIGHT)
            {
                CurrentRoomX++;
                PreviousDirection = LEFT;
            }

            // check bounds
            Debug.Log("X: " + CurrentRoomX + " Y: " + CurrentRoomY);
            if (CurrentRoomY >= RoomObjects.Length || CurrentRoomY < 0)
            {
                CurrentRoomY = prevY;
                Debug.LogError("Escaped Vertical Bounds");
            }
            else if (CurrentRoomX >= RoomObjects[CurrentRoomY].Length || CurrentRoomX < 0)
            {
                CurrentRoomX = prevX;
                Debug.LogError("Escaped Horizontal Bounds");
            }
            // if in bounds, move was allowed and new room needs to load
            else
            {
                // unload previous
                UnloadPrevious(prevX, prevY);

                // load current
                LoadingRoom = true;
                LoadCurrentRoom();
            }
        }
    }
}
