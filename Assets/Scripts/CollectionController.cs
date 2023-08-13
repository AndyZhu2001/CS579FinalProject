using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item{
    public string name;
    public string description;
    //public Sprite itemObject;

}

public class CollectionController : MonoBehaviour
{
    // Start is called before the first frame update

    public Item item;
    public float healthChange;
    public float moveSpeedChange;
    public float attackSpeedChange;
    public float bulletSizeChange;
    void Start()
    {
        //GetComponent<SpriteRenderer>().sprite = item.itemObject;
        Destroy(GetComponent<BoxCollider>());
        //gameObject.AddComponent<BoxCollider>().convex = true;
        gameObject.AddComponent<BoxCollider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player"){
            Debug.Log(item.name);
            GameController.HealPlayer(healthChange);
            GameController.MoveSpeedChange(moveSpeedChange);
            GameController.FireRateChange(attackSpeedChange);
            GameController.BulletSizeChange(bulletSizeChange);
            Destroy(gameObject);
        }
    }
}
