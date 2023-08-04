using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float lifeTime;
    public bool isEnemyBullet = false;
    private Vector3 lastPos;
    private Vector3 curPos;
    private Vector3 playerPos;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DeathDelay());
        if(!isEnemyBullet){
            transform.localScale = new Vector3(GameController.BulletSize, GameController.BulletSize, GameController.BulletSize);
        }   
    }

    // Update is called once per frame
    void Update()
    {   
        if(isEnemyBullet){
            curPos = transform.position;
            transform.position = Vector3.MoveTowards(transform.position, playerPos, 5f * Time.deltaTime);
            if(curPos == lastPos){
                Destroy(gameObject);
            }
            lastPos = curPos;
        }
    }

    public void GetPlayer(Transform player){
        playerPos = player.position;
    }

    IEnumerator DeathDelay(){
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Enemy" && !isEnemyBullet){
            other.GetComponent<EnemyController>().Death();
            Destroy(gameObject);
        }
        else if(other.tag == "Player" && isEnemyBullet){
            GameController.DamagePlayer(1);
            Destroy(gameObject);
        }
    }
}
