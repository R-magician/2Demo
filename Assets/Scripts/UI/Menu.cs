//管理Main场景的脚本

using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Menu : MonoBehaviour
{
    //开始游戏按键
    public GameObject newGameButton;

    private void OnEnable()
    {
        //EventSystem只能有一个，单例模式 
        EventSystem.current.SetSelectedGameObject(newGameButton);
    }

    //退出游戏
    public void ExitGame()
    {
        Debug.Log("退出游戏！");
        Application.Quit();
    }
}
