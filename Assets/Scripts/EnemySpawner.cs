using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]
    public struct Spawnable{
        public GameObject gameObject;
        public float weight;
    }

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
        /*
        //Debug.Log("here");
        if(items[chosenIndex].gameObject.GetComponent<EnemyController>().enemyType == EnemyType.Melee){
            
            GameObject i = Instantiate(items[chosenIndex].gameObject, transform.position + new Vector3(0, 1, 0), Quaternion.identity) as GameObject;
        }
        else{
            
            GameObject i = Instantiate(items[chosenIndex].gameObject, transform.position + new Vector3(0, 2, 0), Quaternion.identity) as GameObject;
        }*/
        StartCoroutine(InstantiateWithDelay(chosenIndex));
        
    }

    IEnumerator InstantiateWithDelay(int chosenIndex) 
    {

        // 等待0.5秒
        yield return new WaitForSeconds(0.5f);

        if(items[chosenIndex].gameObject.GetComponent<EnemyController>().enemyType == EnemyType.Melee){
            
            GameObject i = Instantiate(items[chosenIndex].gameObject, transform.position + new Vector3(0, 1, 0), Quaternion.identity) as GameObject;
        }
        else{
            
            GameObject i = Instantiate(items[chosenIndex].gameObject, transform.position + new Vector3(0, 2, 0), Quaternion.identity) as GameObject;
        }
    }
}
