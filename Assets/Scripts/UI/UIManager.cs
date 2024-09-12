//UI 管理
using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    //获取到血量物体
    public PlayerStatBar playerStatBar;
    
    [Header("事件监听")]
    public CharacterEventSO healthEvent;
    //卸载场景事件监听
    public SceneLoadEventSO unloadedSceneEvent;
    //加载场景事件监听
    public VoidEventSO loadDataEvent;
    //游戏结束监听
    public VoidEventSO gameOverEvent;
    //返回首页监听
    public VoidEventSO backToMainEvent;

    [Header("组件")]
    //面板组件
    public GameObject gameOverPanel;
    //重新开始
    public GameObject restartBtn;
    
    private void OnEnable()
    {
        //受伤事件监听    
        healthEvent.OnEventRaised += OnHealthEvent;
        //卸载场景监听
        unloadedSceneEvent.LoadRequestEvent += OnUnLoadEvent;
        //加载场景监听
        loadDataEvent.OnEventRaised+=OnLoadEvent;
        //游戏结束监听
        gameOverEvent.OnEventRaised += OnGameOverEvent;
        //返回首页监听
        backToMainEvent.OnEventRaised += OnLoadEvent;
    }

    private void OnDisable()
    {
        //取消事件
        healthEvent.OnEventRaised -= OnHealthEvent;
        unloadedSceneEvent.LoadRequestEvent -= OnUnLoadEvent;
        loadDataEvent.OnEventRaised -=OnLoadEvent;
        gameOverEvent.OnEventRaised -= OnGameOverEvent;
        backToMainEvent.OnEventRaised -= OnLoadEvent;
    }

    private void OnGameOverEvent()
    {
        //激活GameOver面板
        gameOverPanel.SetActive(true);
        //因为事件系统只会有一个，设置打开面板激活的按钮
        EventSystem.current.SetSelectedGameObject(restartBtn);
    }

    private void OnLoadEvent()
    {
        //关闭GameOver面板
        gameOverPanel.SetActive(false);
    }

    //显示血条
    private void OnUnLoadEvent(GameSceneSO arg0, Vector3 arg1, bool arg2)
    {
        var isMenu = arg0.sceneType == SceneType.Menu;
        playerStatBar.gameObject.SetActive(!isMenu);
    }

    //受伤事件
    private void OnHealthEvent(Character character)
    {
        //受伤百分比
        var persentage = character.currentHealth / character.maxHealth;
        //传递血量百分比
        playerStatBar.OnHealthChange(persentage);
    }
}
