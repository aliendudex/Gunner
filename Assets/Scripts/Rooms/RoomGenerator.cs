using UnityEngine;
using ED262C;
using UnityEngine.SceneManagement;

public class RoomGenerator : MonoBehaviour
 {
     [Header("Rooms")]
     [SerializeField] private GameObject initialRoom;
     [SerializeField] private GameObject[] roomPrefabs;
     [SerializeField] private GameObject finalRoomPrefab;
     [SerializeField] private int maxRooms = 5;

     [SerializeField] private RoomMap roomMap;
     [SerializeField] private MiniMapUI miniMapUI;
     PlayerController playerController;

     private int currentRoomID = 0;
     private SimpleArrayQueue<RoomData> roomQueue;
     private RoomController currentRoom;

     private int generatedRooms;

     private void Start()
     {
         roomQueue = new SimpleArrayQueue<RoomData>();

         //GenerateRoomQueue();

         SpawnInitialRoom();
     }

  
    private int GetOppositeDoor(int direction)
    {
        switch (direction)
        {
            case 1: 
                return 3;
            case 2: 
                return 4;
            case 3: 
                return 1;
            case 4: 
                return 2;
            default: 
                return 0;
        }
    }

    private GameObject GetRandomRoomPrefab(int forbiddenDirection)
    {
        GameObject[] validRooms = new GameObject[roomPrefabs.Length];
        int validCount = 0;

        foreach (GameObject prefab in roomPrefabs)
        {
            if (prefab == null) continue;

            RoomController controller = prefab.GetComponent<RoomController>();

            if (controller == null) continue;

            if (!controller.HasDoorDirection(forbiddenDirection))
            {
                validRooms[validCount] = prefab;
                validCount++;
            }
        }

        if (validCount == 0)
        {
            Debug.LogError("No hay salas disponibles para esta direccion.");
            return null;
        }

        //Mezcla rooms validas
        for (int i = validCount - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);

            GameObject temp = validRooms[i];
            validRooms[i] = validRooms[j];
            validRooms[j] = temp;
        }

        //Los candidatos vana a la queue
        roomQueue = new SimpleArrayQueue<RoomData>();

        for (int i = 0; i < validCount; i++)
        {
            roomQueue.Enqueue(new RoomData(validRooms[i]));
        }

        return roomQueue.Dequeue().roomPrefab;
    }
    private void SpawnInitialRoom()
     {
        if (initialRoom == null)
        {
             Debug.LogError("No se asigno Initial Room.");
             return;
        }

        GameObject room = Instantiate(initialRoom, new Vector3(-17.049118f, -3.69844198f, 0), Quaternion.identity);

        currentRoom = room.GetComponent<RoomController>();

         if (currentRoom == null)
         {
             Debug.LogError("La sala inicial no tiene RoomController.");
             return;
         }

         currentRoomID = 1;
         generatedRooms = 1;

         currentRoom.Initialize(this);
         roomMap.DiscoverRoom(currentRoomID);
         miniMapUI.AddRoom(currentRoomID, 0);
     }

     public void GenerateNextRoom(RoomDoor door, Transform player, int doorUsed)
     {
         
         if (generatedRooms >= maxRooms)
         {
             Debug.Log("Final room reached. Defeat the boss to win!");
             return;
         }

         

         //Transform nextRoomPoint = door.NextRoomPoint;
         Transform nextRoomPoint = door.NextRoomPoint;

        int forbiddenDirection = GetOppositeDoor(doorUsed);

        GameObject selectedPrefab;

        if (generatedRooms + 1 >= maxRooms)
        {
            selectedPrefab = finalRoomPrefab;
        }
        else
        {
            selectedPrefab = GetRandomRoomPrefab(forbiddenDirection);
        }

        if (selectedPrefab == null)
            return;

        if (selectedPrefab == null)
            return;

         if (nextRoomPoint == null)
         {
             Debug.LogError("La puerta no tiene Next Room Point.");
             return;
         }

         //RoomData roomData = roomQueue.Dequeue();

         //GameObject newRoom = Instantiate(
         //roomData.roomPrefab,
         //roomData.roomPrefab.transform.position,
         //roomData.roomPrefab.transform.rotation
         //);
         GameObject newRoom = Instantiate(
         selectedPrefab,
         selectedPrefab.transform.position,
         selectedPrefab.transform.rotation
         );

         RoomController newRoomController = newRoom.GetComponent<RoomController>();

         if (newRoomController == null)
         {
             Debug.LogError("El prefab de la sala no tiene RoomController.");
             Destroy(newRoom);
             return;
         }
         GameObject oldRoom = currentRoom.gameObject;
         newRoomController.Initialize(this);
         currentRoom = newRoomController;

         generatedRooms++;
         currentRoomID++;

         roomMap.DiscoverRoom(currentRoomID);

         if (doorUsed == 1)
         {
            roomMap.AddConnection(currentRoomID, RoomDirection.Right);
         }

         else if (doorUsed == 2)
         {
            roomMap.AddConnection(currentRoomID, RoomDirection.Up);
         }
         if (doorUsed == 3)
         {
             roomMap.AddConnection(currentRoomID, RoomDirection.Left);
         }

         else if (doorUsed == 4)
         {
             roomMap.AddConnection(currentRoomID, RoomDirection.Down);
         }
         miniMapUI.AddRoom(currentRoomID, doorUsed);
         player.position = newRoom.transform.position;

         Destroy(oldRoom);
     }
 }