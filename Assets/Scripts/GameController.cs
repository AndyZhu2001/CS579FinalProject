using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public static GameController instance;

    private static float health = 6;
    private static float maxHealth = 6;
    public static float moveSpeed = 5f;
    private static float fireRate = 0.5f;
    private static float bulletSize = 0.5f;

    public static float Health{get => health; set => health = value;}
    public static float MaxHealth{get => maxHealth; set => maxHealth = value;}
    public static float MoveSpeed{get => moveSpeed; set => moveSpeed = value;}
    public static float FireRate{get => fireRate; set => fireRate = value;}
    public static float BulletSize{get => bulletSize; set => bulletSize = value;}

    private void Awake(){
        if(instance == null){
            instance = this;
        }
    }

    public static void DamagePlayer(float damage){
        health -= damage;
        if(health < 0){
            KillPlayer();
        }
    }

    public static void HealPlayer(float healAmount){
        health = Mathf.Min(maxHealth, health + healAmount);
    }

    public static void MoveSpeedChange(float moveSpeedChange){
        moveSpeed += moveSpeedChange;
    }

    public static void FireRateChange(float fireRateChagne){
        fireRate -= fireRateChagne;
    }

    public static void BulletSizeChange(float bulletSizeChange){
        bulletSize += bulletSizeChange;
    }

    private static void KillPlayer(){

    }
}
