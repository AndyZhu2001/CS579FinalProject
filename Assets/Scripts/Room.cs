using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{

    public int Width;
    public int Length;
    public int X;
    public int Z;

    private bool updatedDoors = false;

    public Room(int x, int z){
        X = x;
        Z = z;
    }

    public Door leftDoor;
    public Door rightDoor;
    public Door topDoor;
    public Door bottomDoor;

    public List<Door> doors = new List<Door>();

    // Start is called before the first frame update
    void Start()
    {
        if(RoomController.instance == null){
            Debug.Log("You pressed play in the wrong scene!");
            return;
        }

        Door[] ds = GetComponentsInChildren<Door>();

        foreach(Door d in ds){

            doors.Add(d);

            switch(d.doorType){
                case Door.DoorType.left:
                    leftDoor = d;
                break;
                case Door.DoorType.right:
                    rightDoor = d;
                break;
                case Door.DoorType.top:
                    topDoor = d;
                break;
                case Door.DoorType.bottom:
                    bottomDoor = d;
                break;
            }
        }

        RoomController.instance.RegisterRoom(this);
    }

    void Update(){
        if(name.Contains("Boss") && !updatedDoors){
            RemoveUnconnectedDoors();
            updatedDoors = true;
        }
    }

    public void RemoveUnconnectedDoors(){
        foreach(Door door in doors){
            switch(door.doorType){
                case Door.DoorType.left:
                    if(GetLeft() == null){
                        door.gameObject.SetActive(false);
                    }
                break;
                case Door.DoorType.right:
                    if(GetRight() == null){
                        door.gameObject.SetActive(false);
                    }
                break;
                case Door.DoorType.top:
                    if(GetTop() == null){
                        door.gameObject.SetActive(false);
                    }
                break;
                case Door.DoorType.bottom:
                    if(GetBottom() == null){
                        door.gameObject.SetActive(false);
                    }
                break;
            }
        }
    }

    public Room GetLeft(){
        if(RoomController.instance.DoesRoomExist(X + 1, Z)){
            return RoomController.instance.FindRoom(X + 1, Z);
        }
        return null;
    }
    public Room GetRight(){
        if(RoomController.instance.DoesRoomExist(X - 1, Z)){
            return RoomController.instance.FindRoom(X - 1, Z);
        }
        return null;
    }
    public Room GetBottom(){
        if(RoomController.instance.DoesRoomExist(X, Z + 1)){
            return RoomController.instance.FindRoom(X, Z + 1);
        }
        return null;
    }
    public Room GetTop(){
        if(RoomController.instance.DoesRoomExist(X, Z - 1)){
            return RoomController.instance.FindRoom(X, Z - 1);
        }
        return null;
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(Width, 0, Length));
    }

    public Vector3 GetRoomCentre(){
        return new Vector3(X * Width, 0, Z * Length);

    }
}
