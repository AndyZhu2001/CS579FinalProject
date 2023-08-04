using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    //public GameObject player;
    public float speed;
    Rigidbody rigidbody;
    //public CharacterController controller;
    public float bulletSpeed;
    public GameObject bulletPrefab;
    private float lastFire;
    private float fireDelay;
    private float rotateSpeed = 75f;


    // Start is called before the first frame update
    void Start()
    {   
        rigidbody = GetComponent<Rigidbody>();
        //controller = transform.GetComponent<CharacterController>();
    }

    void Update()
    {
        //Debug.Log("player's transform: " + transform);
        fireDelay = GameController.FireRate;
        speed = GameController.MoveSpeed;


        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        float mouseRotate = Input.GetAxis("Mouse X");

        //controller.Move(new Vector3(horizontal * speed * Time.deltaTime, 0, vertical * speed * Time.deltaTime));

        if(Input.GetAxis("Fire") > 0 && Time.time > lastFire + fireDelay){
            //Vector3 x = transform.forward();
            Shoot();
            lastFire = Time.time;
        }


        /*
        if(Mathf.Abs(Input.GetAxis("Mouse X")) > 0.5f){
            transform.Rotate(0, mouseRotate * rotateSpeed * Time.deltaTime, 0);
            //lastRotate = Time.time;
        }
        */

        if (Input.GetKey(KeyCode.W) | Input.GetKey(KeyCode.UpArrow)) //前
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S) | Input.GetKey(KeyCode.DownArrow)) //后
        {
            transform.Translate(Vector3.forward * - speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A) | Input.GetKey(KeyCode.LeftArrow)) //左
        {
            //transform.Translate(Vector3.right * - speed * Time.deltaTime);
            transform.Rotate(0, -rotateSpeed * Time.deltaTime, 0);
        }
        if (Input.GetKey(KeyCode.D) | Input.GetKey(KeyCode.RightArrow)) //右
        {
            //transform.Translate(Vector3.right * speed * Time.deltaTime);
            transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);
        }


    } 

    void Shoot(){
        Vector3 offsetPos = transform.position + transform.forward;
        //offsetPos.z += 2;
        GameObject bullet = Instantiate(bulletPrefab, offsetPos, transform.rotation) as GameObject;
        bullet.AddComponent<Rigidbody>().useGravity = false;
        Vector3 forward = transform.forward;
        forward.x = forward.x * bulletSpeed;
        forward.y = forward.y * bulletSpeed;
        forward.z = forward.z * bulletSpeed;
        bullet.GetComponent<Rigidbody>().velocity = forward;
        Destroy(bullet, 2);
    }

}
