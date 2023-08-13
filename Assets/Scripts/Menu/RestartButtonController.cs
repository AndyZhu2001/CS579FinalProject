using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButtonController : MonoBehaviour
{
    public void OnRestartButtonClick()
    {
        // 重新加载当前激活的场景
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        GameController.Health = 6;
        GameController.instance.WinUI.SetActive(false);
        GameController.instance.LoseUI.SetActive(false);
        BossHeartController.health = 10;
        BossHeartController.startFire = false;
        Time.timeScale = 1;
    }
}
