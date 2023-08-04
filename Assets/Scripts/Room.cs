using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{

    public int Width;
    public int Length;
    public int X;
    public int Z;

    // Start is called before the first frame update
    void Start()
    {
        if(RoomController.instance == null){
            Debug.Log("You pressed play int he wrong scene!");
            return;
        }
        RoomController.instance.RegisterRoom(this);
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(Width, 0, Length));
    }

    public Vector3 GetRoomCentre(){
        return new Vector3(X * Width, 0, Z * Length);

    }
}
