using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomTrigger : MonoBehaviour
{

    public GameObject healthBarUI;
    // Start is called before the first frame update
    void Start()
    {
        healthBarUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player"){
            healthBarUI.SetActive(true);
            BossHeartController.startFire = true;
        }
    }
}
