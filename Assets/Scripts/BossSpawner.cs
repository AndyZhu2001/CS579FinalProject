using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    [System.Serializable]
    public struct Spawnable{
        public GameObject gameObject;
        public float weight;
    }
    /*
    Vector3 treasureRoomTransform;
    public ItemSpawner(Vector3 roomTransform){
        treasureRoomTransform = roomTransform;
    }
    */
    public List<Spawnable> items = new List<Spawnable>();
    float totalWeight;

    void Awake(){
        totalWeight = 0;
        foreach(var spawnable in items){
            totalWeight += spawnable.weight;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        float pick = Random.value * totalWeight;
        int chosenIndex = 0;
        float cumulativeWeight = items[0].weight;

        while(pick > cumulativeWeight && chosenIndex < items.Count - 1){
            chosenIndex++;
            cumulativeWeight += items[chosenIndex].weight;

        }
        //Debug.Log("bosshere");
        Debug.Log(items[chosenIndex].gameObject.name);
        GameObject i = Instantiate(items[chosenIndex].gameObject, transform.position, Quaternion.identity) as GameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
