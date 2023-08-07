using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DungeonCrawler : MonoBehaviour
{
    public Vector3Int Position {get; set;}
    public DungeonCrawler(Vector3Int startPos){
        Position = startPos;
    }

    public Vector3Int Move(Dictionary<Direction, Vector3Int> directionMovementMap){
        Direction toMove =(Direction)Random.Range(0, directionMovementMap.Count);
        Position += directionMovementMap[toMove];
        return Position;
    }
}
