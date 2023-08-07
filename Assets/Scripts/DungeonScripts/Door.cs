using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public enum DoorType{
        left, right, top, bottom
    }

    public DoorType doorType;
    public GameObject parentObject;
    public Room room;
    public Transform parentTransform;

    void Start(){
        parentTransform = gameObject.transform.parent;
        parentObject = parentTransform.gameObject;
        room = parentObject.GetComponent<Room>();
    }

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player"){
            switch(doorType){
                case DoorType.left:
                    Room leftRoom = GetLeft();
                    Door rightDoor = leftRoom.rightDoor;
                    other.transform.position = rightDoor.transform.position + new Vector3(1, -1.5f, 0);
                break;
                case DoorType.right:
                    Room rightRoom = GetRight();
                    Door LeftDoor = rightRoom.leftDoor;
                    other.transform.position = LeftDoor.transform.position + new Vector3(-1, -1.5f, 0);
                break;
                case DoorType.top:
                    Room topRoom = GetTop();
                    Door BottomDoor = topRoom.bottomDoor;
                    other.transform.position = BottomDoor.transform.position + new Vector3(0, -1.5f, -1);
                break;
                case DoorType.bottom:
                    Room bottomRoom = GetBottom();
                    Door topDoor = bottomRoom.topDoor;
                    other.transform.position = topDoor.transform.position + new Vector3(0, -1.5f, 1);
                break;
            }
        }
    }

    public Room GetRight(){
        if(RoomController.instance.DoesRoomExist(room.X - 1, room.Z)){
            return RoomController.instance.FindRoom(room.X - 1, room.Z);
        }
        return null;
    }
    public Room GetLeft(){
        if(RoomController.instance.DoesRoomExist(room.X + 1, room.Z)){
            return RoomController.instance.FindRoom(room.X + 1, room.Z);
        }
        return null;
    }
    public Room GetTop(){
        if(RoomController.instance.DoesRoomExist(room.X, room.Z - 1)){
            return RoomController.instance.FindRoom(room.X, room.Z - 1);
        }
        return null;
    }
    public Room GetBottom(){
        if(RoomController.instance.DoesRoomExist(room.X, room.Z + 1)){
            return RoomController.instance.FindRoom(room.X, room.Z + 1);
        }
        return null;
    }
}
