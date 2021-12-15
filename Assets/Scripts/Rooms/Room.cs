using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Room : MonoBehaviour
{
    // enemy data
    public List<Enemy> Enemies;
    public List<Vector3> EnemyPositions;
    public List<GameObject> LoadedEnemies;
    public GameObject TurretPrefab;
    public int NumEnemies;

    // item data
    public List<GameObject> Collectibles;
    public List<Vector3> CollectiblePositions;
    public List<GameObject> LoadedCollectibles;
    public GameObject PistolPrefab, RiflePrefab, HealthPickupPrefab, ClonePodPrefab;

    // clone spawn locations
    public List<Vector2> Spawns; // first 4 are bottom, then top, left, right

    // doors
    public GameObject Doors;
    public TileBase ClosedDoor, OpenDoor;

    // room manager
    private RoomManager RM;
    private bool NeedsUpdate = false;
    public bool IsCleared;

    // camera controls
    public bool IsLarge;
    public float CameraBoundRight, CameraBoundBottom, CameraBoundLeft, CameraBoundTop;

    private void Awake()
    {
        RM = FindObjectOfType<RoomManager>();
    }

    public void InitialLoadLevel()
    {
        // loading enemies
        LoadedEnemies = new List<GameObject>();
        NumEnemies = Enemies.Count;
        for (int i = 0; i < Enemies.Count; i++)
        {
            GameObject currentEnemy = null;
            if (Enemies[i] is Turret)
            {
                currentEnemy = Instantiate(TurretPrefab, EnemyPositions[i], Quaternion.identity);
            }
            LoadedEnemies.Add(currentEnemy);
            // other checks for other types of enemies
        }

        // loading collectibles
        LoadedCollectibles = new List<GameObject>();
        for (int i = 0; i < Collectibles.Count; i++)
        {
            GameObject item = null;
            if (Collectibles[i] == PistolPrefab)
            {
                item = Instantiate(PistolPrefab, CollectiblePositions[i], Quaternion.identity);
            }
            else if (Collectibles[i] == RiflePrefab)
            {
                item = Instantiate(RiflePrefab, CollectiblePositions[i], Quaternion.identity);
            }
            else if (Collectibles[i] == HealthPickupPrefab)
            {
                item = Instantiate(HealthPickupPrefab, CollectiblePositions[i], Quaternion.identity);
            }
            else if (Collectibles[i] == ClonePodPrefab)
            {
                item = Instantiate(ClonePodPrefab, CollectiblePositions[i], Quaternion.identity);
            }
            LoadedCollectibles.Add(item);
        }
    }

    public void LoadLevel(GameObject[] clones, int direction)
    {
        // loading enemies
        for (int i = 0; i < LoadedEnemies.Count; i++)
        {
            if (LoadedEnemies[i] != null)
            {
                LoadedEnemies[i].SetActive(true);
            }
        }

        // loading collectibles
        for (int i = 0; i < LoadedCollectibles.Count; i++)
        {
            if (LoadedCollectibles[i] != null)
            {
                LoadedCollectibles[i].SetActive(true);
            }
        }

        // move clones to spawn positions
        for (int i = 0; i < clones.Length; i++)
        {
            CloneMovement currentClone = clones[i].GetComponent<CloneMovement>();
            currentClone.MoveToPosition(Spawns[i + (direction * 4)]);
        }

        NeedsUpdate = true;
    }

    public void UnloadLevel()
    {
        // removing enemies
        for (int i = 0; i < LoadedEnemies.Count; i++)
        {
            if (LoadedEnemies[i] != null)
            {
                LoadedEnemies[i].SetActive(false);
            }
        }

        // removing collectibles
        for (int i = 0; i < LoadedCollectibles.Count; i++)
        {
            if (LoadedCollectibles[i] != null)
            {
                // if not held by player, remove weapon
                if (LoadedCollectibles[i].transform.parent == null)
                {
                    LoadedCollectibles[i].SetActive(false);
                }
            }
            
        }

        //gameObject.SetActive(false);
    }

    private void Update()
    {
        for (int i = 0; i < LoadedEnemies.Count; i++)
        {
            GameObject obj = LoadedEnemies[i];

            Enemy e = null;
            if (obj == null)
            {
                // nothing to do
            }
            else if (obj.GetComponent<Turret>() != null)
            {
                e = obj.GetComponent<Turret>();
            }
            // add other types of enemies
                
            if (e != null && e.ShouldBeRemoved())
            {
                LoadedEnemies[i] = null;
                Destroy(obj);
                NumEnemies--;
            }
        }

        if (NumEnemies == 0)
        {
            OpenDoors();
        }
    }

    private void FixedUpdate()
    {
        if (NeedsUpdate)
        {
            RM.FinishLoading();
            NeedsUpdate = false;
        }
    }

    private void OpenDoors()
    {
        Tilemap tiles = Doors.GetComponent<Tilemap>();
        tiles.SwapTile(ClosedDoor, OpenDoor);

        CompositeCollider2D collider = Doors.GetComponent<CompositeCollider2D>();
        collider.isTrigger = true;

        IsCleared = true;
    }

    public void RemoveCollected(GameObject obj)
    {
        LoadedCollectibles.Remove(obj);
    }

    public void AddDroppedCollectible(GameObject obj)
    {
        if (!LoadedCollectibles.Contains(obj))
        {
            LoadedCollectibles.Add(obj);
        }   
    }
}
