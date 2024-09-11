//UI 管理
using System;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    //获取到血量物体
    public PlayerStatBar playerStatBar;
    
    [Header("事件监听")]
    public CharacterEventSO healthEvent;
    //场景事件监听
    public SceneLoadEventSO LoadEvent;

    private void OnEnable()
    {
        //注册事件    
        healthEvent.OnEventRaised += OnHealthEvent;
        LoadEvent.LoadRequestEvent += OnLoadEvent;
    }

    private void OnDisable()
    {
        //取消事件
        healthEvent.OnEventRaised -= OnHealthEvent;
        LoadEvent.LoadRequestEvent -= OnLoadEvent;
    }

    //显示血条
    private void OnLoadEvent(GameSceneSO arg0, Vector3 arg1, bool arg2)
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
