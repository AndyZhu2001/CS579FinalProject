using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    
    public Room room;

    [System.Serializable]
    public struct Grid{
        public int columns, rows;
        public float verticalOffset, horizontalOffset;

    }
    
    public Grid grid;
    public GameObject gridTile;
    public List<Vector3> availablePoints = new List<Vector3>();

    void Awake(){
        room = GetComponentInParent<Room>();
        grid.columns = room.Width - 5;
        grid.rows = room.Length - 5;
        GenerateGrid();
    }

    public void GenerateGrid(){
        grid.verticalOffset += room.transform.localPosition.z;
        grid.horizontalOffset += room.transform.localPosition.x;

        for(int z = 0; z < grid.rows; z++){
            for(int x = 0; x < grid.columns; x++){
                GameObject go = Instantiate(gridTile, transform);
                go.transform.SetParent(transform, false);
                go.transform.localScale = new Vector3((float)1/1500, (float)1/1200, (float)1);
                go.transform.position = new Vector3(x - (grid.columns - grid.horizontalOffset), 1, z - (grid.rows - grid.verticalOffset));
                //go.transform.localScale = new Vector3((float)1/1500, (float)1/1200, (float)1/100);
                go.name = "X: " + x + ", Z: " + z;
                availablePoints.Add(go.transform.position);
            }
        }

        GetComponentInParent<ObjectRoomSpawner>().InitialiseObjectSpawning();
    }
}
