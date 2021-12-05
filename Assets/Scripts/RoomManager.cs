using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public GameObject RoomToLoad;
    private Room LoadedRoom;

    private CloneManager Clones;

    public const int DOWN = 0, UP = 1, LEFT = 2, RIGHT = 3;

    public GameObject[][] RoomTemplates;
    public GameObject[][] RoomObjects;

    public GameObject Room1, Room2;

    public int CurrentRoomX, CurrentRoomY;
    public int PreviousDirection;

    private void Awake()
    {
        Clones = FindObjectOfType<CloneManager>();

        RoomTemplates = new GameObject[][] { new GameObject[] {Room1, Room2}, new GameObject[] {Room1, Room2} };
        RoomObjects = new GameObject[RoomTemplates.Length][];
        
        for (int row = 0; row < RoomObjects.Length; row++)
        {
            RoomObjects[row] = new GameObject[RoomTemplates[0].Length];
        }

        InitialRoomLoad();

        RoomToLoad = RoomTemplates[CurrentRoomY][CurrentRoomX];

        LoadCurrentRoom();
    }

    /*
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            LoadedRoom.UnloadLevel();
            Destroy(LoadedRoom.gameObject);
            Load();
        }
    }
    */

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
            }
        }
    }

    private void LoadCurrentRoom()
    {
        /*
        GameObject room = Instantiate(RoomToLoad, Vector3.zero, Quaternion.identity);
        LoadedRoom = room.GetComponent<Room>();
        LoadedRoom.LoadLevel(Clones.GetClones().ToArray(), PreviousDirection);
        */

        GameObject room = RoomObjects[CurrentRoomY][CurrentRoomY];
        room.SetActive(true);
        LoadedRoom = room.GetComponent<Room>();
        LoadedRoom.LoadLevel(Clones.GetClones().ToArray(), PreviousDirection);
    }

    public void MoveRoom(int direction)
    {
        int prevX = CurrentRoomX, prevY = CurrentRoomY;

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
            CurrentRoomY--;
            PreviousDirection = RIGHT;
        }
        else if (direction == RIGHT)
        {
            CurrentRoomY++;
            PreviousDirection = LEFT;
        }

        // check bounds
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
            Room prev = RoomToLoad.GetComponent<Room>();
            prev.UnloadLevel();
            RoomToLoad = RoomObjects[CurrentRoomY][CurrentRoomX];
            LoadCurrentRoom();
        }
    }
}
