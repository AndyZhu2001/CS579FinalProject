using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueButtonController : MonoBehaviour
{

    public GameObject Canvas;
    public GameObject Menu;
    
    public void OnContinueButtonClick(){
        Canvas.GetComponent<MenuController>().isActive = false;
        Time.timeScale = 1;
        Menu.SetActive(false);
    }
}
