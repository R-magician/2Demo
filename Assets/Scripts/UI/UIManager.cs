using System;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    //获取到血量物体
    public PlayerStatBar playerStatBar;
    
    [Header("事件监听")]
    public CharacterEventSO healthEvent;

    private void OnEnable()
    {
        //注册事件    
        healthEvent.OnEventRaised += OnHealthEvent;
    }

    private void OnDisable()
    {
        //取消事件
        healthEvent.OnEventRaised -= OnHealthEvent;
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
