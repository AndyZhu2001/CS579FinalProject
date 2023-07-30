using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform playerPos;
    public Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        playerPos = player.transform;
        offset = new Vector3(0, 2, 0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = playerPos.position + offset;
        //transform.rotation = 
    }
}
