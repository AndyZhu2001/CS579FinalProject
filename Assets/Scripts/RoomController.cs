using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class RoomInfo{
    public string name;
    public int X;
    public int Z;

}

public class RoomController : MonoBehaviour
{
    public static RoomController instance;

    string currentWorldName = "Dungeon";

    RoomInfo currentLoadRoomData;

    //public static Room treasureRoom;

    Queue<RoomInfo> loadRoomQueue = new Queue<RoomInfo>();

    public List<Room> loadedRooms = new List<Room>();

    public Room currRoom;

    bool isLoadingRoom = false;

    bool spawnedBossRoom = false;

    bool spawnedTreasureRoom = false;

    bool updatedRooms = false;

    void Awake(){
        instance = this;
    }

    void Start(){
        
    }

    void Update(){
        UpdateRoomQueue();
    }

    void UpdateRoomQueue(){
        if(isLoadingRoom){
            return;
        }

        if(loadRoomQueue.Count == 0){
            if(!spawnedBossRoom){
                StartCoroutine(SpawnBossRoom());
            }
            else if(!spawnedTreasureRoom){
                StartCoroutine(SpawnTreasureRoom());
            }
            else if(spawnedBossRoom && spawnedTreasureRoom && !updatedRooms){
                foreach(Room room in loadedRooms){
                    room.RemoveUnconnectedDoors();
                }
                updatedRooms = true;
            }
            return;
        }

        currentLoadRoomData = loadRoomQueue.Dequeue();
        isLoadingRoom = true;
        StartCoroutine(LoadRoomRoutine(currentLoadRoomData));
    }

    IEnumerator SpawnBossRoom(){
        spawnedBossRoom = true;
        yield return new WaitForSeconds(0.2f);
        if(loadRoomQueue.Count == 0){
            Room bossRoom = loadedRooms[loadedRooms.Count - 1];
            Room tempRoom = new Room(bossRoom.X, bossRoom.Z);
            //Destroy(bossRoom.gameObject.)
            foreach (Transform child in bossRoom.gameObject.transform) {
                Destroy(child.gameObject);
            }
            Destroy(bossRoom.gameObject);
            var roomToRemove = loadedRooms.Single(r => r.X == tempRoom.X && r.Z == tempRoom.Z);
            loadedRooms.Remove(roomToRemove);
            LoadRoom("Boss", tempRoom.X, tempRoom.Z);
        }
    }

    IEnumerator SpawnTreasureRoom(){
        spawnedTreasureRoom = true;
        yield return new WaitForSeconds(0.4f);
        if(loadRoomQueue.Count == 0){
            Room treasureRoom = loadedRooms[loadedRooms.Count - 3];
            Room tempRoom = new Room(treasureRoom.X, treasureRoom.Z);
            foreach (Transform child in treasureRoom.gameObject.transform) {
                Destroy(child.gameObject);
            }
            Destroy(treasureRoom.gameObject);
            var roomToRemove = loadedRooms.Single(r => r.X == tempRoom.X && r.Z == tempRoom.Z);
            loadedRooms.Remove(roomToRemove);
            LoadRoom("Treasure", tempRoom.X, tempRoom.Z);
        }
    }

    public void LoadRoom(string name, int x, int z){
        if(DoesRoomExist(x, z)){
            Debug.Log("Exist");
            return;
        }
        RoomInfo newRoomData = new RoomInfo();
        newRoomData.name = name;
        newRoomData.X = x;
        newRoomData.Z = z;

        loadRoomQueue.Enqueue(newRoomData);
    }

    IEnumerator LoadRoomRoutine(RoomInfo info){
        string roomName = currentWorldName + info.name;
        AsyncOperation loadRoom = SceneManager.LoadSceneAsync(roomName, LoadSceneMode.Additive);

        while(loadRoom.isDone == false){
            yield return null;
        }
    }

    public void RegisterRoom(Room room){
        if(!DoesRoomExist(currentLoadRoomData.X, currentLoadRoomData.Z)){
            room.transform.position = new Vector3(
                currentLoadRoomData.X * room.Width,
                0,
                currentLoadRoomData.Z * room.Length
            );

            //room.currRoomTransform = room.transform.position;

            room.X = currentLoadRoomData.X;
            room.Z = currentLoadRoomData.Z;
            room.name = currentWorldName + "-" + currentLoadRoomData.name + " " + room.X + ", " + room.Z;
            room.transform.parent = transform;

            isLoadingRoom = false;

            loadedRooms.Add(room);
        }
        else{
            Destroy(room.gameObject);
            isLoadingRoom = false;
        }
    }
    
    public bool DoesRoomExist(int x, int z){
        return loadedRooms.Find(item => item.X == x && item.Z == z) != null;
    }

    public Room FindRoom(int x, int z){
        return loadedRooms.Find(item => item.X == x && item.Z == z);
    }

    public string GetRandomRoomName(){
        string[] possibleRooms = new string[]{
            "Empty",
            "Basic1"
        };

        return possibleRooms[Random.Range(0, possibleRooms.Length)];
    }
}
