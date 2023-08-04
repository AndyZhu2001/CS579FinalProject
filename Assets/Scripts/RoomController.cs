using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    Queue<RoomInfo> loadRoomQueue = new Queue<RoomInfo>();

    public List<Room> loadedRooms = new List<Room>();

    bool isLoadingRoom = false;

    void Awake(){
        instance = this;
    }

    void Start(){
        LoadRoom("Empty", 0, 0);
        LoadRoom("Empty", 1, 0);
        LoadRoom("Empty", -1, 0);
        LoadRoom("Empty", 0, 1);
        LoadRoom("Empty", 0, -1);
    }

    void Update(){
       // Debug.Log("here");
        UpdateRoomQueue();
    }

    void UpdateRoomQueue(){
        if(isLoadingRoom){
            return;
        }

        if(loadRoomQueue.Count == 0){
            return;
        }

        currentLoadRoomData = loadRoomQueue.Dequeue();
        isLoadingRoom = true;
        StartCoroutine(LoadRoomRoutine(currentLoadRoomData));
    }

    public void LoadRoom(string name, int x, int z){
        if(DoesRoomExist(x, z)){
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
        room.transform.position = new Vector3(
            currentLoadRoomData.X * room.Width,
            0,
            currentLoadRoomData.Z * room.Length
        );

        room.X = currentLoadRoomData.X;
        room.Z = currentLoadRoomData.Z;
        room.name = currentWorldName + "-" + currentLoadRoomData.name + " " + room.X + ", " + room.Z;
        room.transform.parent = transform;

        isLoadingRoom = false;

        loadedRooms.Add(room);
    }
    
    public bool DoesRoomExist(int x, int z){
        return loadedRooms.Find(item => item.X == x && item.Z == z) != null;
    }
}
