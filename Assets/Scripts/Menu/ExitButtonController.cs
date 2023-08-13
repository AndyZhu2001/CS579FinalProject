using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class ExitButtonController : MonoBehaviour
{
    public void OnButtonClick(){
        #if UNITY_EDITOR
        // 退出编辑器的播放模式
        EditorApplication.isPlaying = false;
        #else
        // 退出构建的游戏
        Application.Quit();
        #endif
    }
}
