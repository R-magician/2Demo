//UI 管理
using System;
using UnityEngine;

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
    }

    private void OnDisable()
    {
        //取消事件
        healthEvent.OnEventRaised -= OnHealthEvent;
        unloadedSceneEvent.LoadRequestEvent -= OnUnLoadEvent;
        loadDataEvent.OnEventRaised -=OnLoadEvent;
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
