using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject Menu;
    public bool isActive = false;
    //public static GameObject WinUI;
    // Start is called before the first frame update
    void Start()
    {
        Menu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("key pressed: " + Input.GetKeyDown(KeyCode.Escape));
        if(Input.GetKeyDown(KeyCode.Escape) && !isActive){
            Menu.SetActive(true);
            isActive = true;
            Time.timeScale = 0;
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && isActive){
            Menu.SetActive(false);
            isActive = false;
            Time.timeScale = 1;
        }

    }
}
