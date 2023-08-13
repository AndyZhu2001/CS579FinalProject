using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRoomSpawner : MonoBehaviour
{
    [System.Serializable]
    public struct RandomSpawner{
        public string name;
        public SpawnerData spawnerData;
    }

    public GridController grid;
    public RandomSpawner[] spawnerData;

    void Start(){
       // grid.GetComponentInChildren<GridController>();
    }

    public void InitialiseObjectSpawning(){
        foreach(RandomSpawner rs in spawnerData){
            SpawnObjects(rs);
        }
    }

    void SpawnObjects(RandomSpawner data){
        int randomIteration = Random.Range(data.spawnerData.minSpawn, data.spawnerData.maxSpawn + 1);

        for(int i = 0; i < randomIteration; i++){
            Debug.Log(data.name);
            if(data.name.Contains("Item")){
                GameObject go = Instantiate(data.spawnerData.itemToSpawn, transform.position + new Vector3(0, 2, 0), Quaternion.identity) as GameObject;
                //go.transform.localScale = new Vector3((float)1/1500, (float)1/1200, (float)1/100);
                //go.transform.localScale = new Vector3(3, 3, 3);
                go.transform.SetParent(transform, true);
            }
            else if(data.name.Contains("Boss")){
                GameObject go = Instantiate(data.spawnerData.itemToSpawn, transform.position + new Vector3(0, 2, 0), Quaternion.identity) as GameObject;
                //go.transform.localScale = new Vector3((float)1/1500, (float)1/1200, (float)1/100);
                //go.transform.localScale = new Vector3(3, 3, 3);
                go.transform.SetParent(transform, true);
            }
            else{
                int randomPos = Random.Range(0, grid.availablePoints.Count - 1);
                GameObject go = Instantiate(data.spawnerData.itemToSpawn, grid.availablePoints[randomPos] + new Vector3(0, 1, 0), Quaternion.identity) as GameObject;
                //go.transform.localScale = new Vector3((float)1/1500, (float)1/1200, (float)1/100);
                go.transform.localScale = new Vector3(3, 3, 3);
                grid.availablePoints.RemoveAt(randomPos);
                go.transform.SetParent(transform, true);
                //Debug.Log("Spawned Object!");
            }
        }
    }
}
