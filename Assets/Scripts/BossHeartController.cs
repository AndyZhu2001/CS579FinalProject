using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHeartController : MonoBehaviour
{

    public static float health = 10;
    public static float maxHealth = 10;
    public float fireRate = 0.5f;
    public float lastFire;
    public int numberOfBullets = 5; // 要发射的子弹数
    public float bulletSpeed = 10f; // 子弹速度
    public float spreadAngle = 360f; // 发射的扇形角度
    public GameObject heartPrefab;
    public GameObject bulletPrefab;
    //public GameObject winUI;
    //public static GameObject boss;

    public static bool startFire = false;
    
    //public static float Health{get => health; set => health = value;}

    // Start is called before the first frame update
    void Start()
    {
        //boss = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > lastFire + fireRate && startFire){
            //Debug.Log("shoot");
            Shoot();
            lastFire = Time.time;
        }
        if(health <= 0){
            Death();
        }
    }

    void Shoot(){
        for (int i = 0; i < numberOfBullets; i++)
        {
            // 计算每个子弹的方向
            // - spreadAngle / 2
            //float angle = spreadAngle / (numberOfBullets - 1) * i;
            //Vector3 direction = Quaternion.Euler(0, angle, 0) * transform.forward;

            float angle = 360f / numberOfBullets * i; // 这里我们将360度平均分成numberOfBullets个部分
            Vector3 direction = Quaternion.Euler(0, angle, 0) * transform.forward;

            // 实例化子弹
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.LookRotation(direction)) as GameObject;
            bullet.AddComponent<Rigidbody>().useGravity = false;
            bullet.GetComponent<BulletController>().isEnemyBullet = true;
            Rigidbody rb = bullet.GetComponent<Rigidbody>();

            // 给子弹添加速度
            if (rb != null)
            {
                //Debug.Log(bullet == null);
                rb.velocity = direction * bulletSpeed;
            }
            Destroy(bullet, 2);
        }
    }


    public void getDemage(float amount){
        health -= amount;
    }

    void Death(){
        GameController.instance.WinUI.SetActive(true);
        Destroy(gameObject);
        Time.timeScale = 0;
    }
}
