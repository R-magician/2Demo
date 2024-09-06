using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//人物属性数值计算
public class Character : MonoBehaviour
{
    [Header("基本属性")]
    //最大血量
    public float maxHealth;

    //当前血量
    public float currentHealth;

    [Header("受伤无敌")]
    //无敌时间
    public float invulnerableDuration;

    //计算器
    private float invulnerableCounter;
    
    //计算器状态
    public bool invulnerab;

    //触发轻量级的对象类型的订阅--在面板上把事件广播出去-其他代码进行事件监听
    public UnityEvent<Character> OnHealthChange;
    
    //自定义unity中的事件--受伤
    public UnityEvent<Transform> onTakeDamage;
    //死亡时触发的事件栈
    public UnityEvent OnDie;

    private void Start()
    {
        currentHealth = maxHealth;
        //更新UI
        OnHealthChange.Invoke(this);
    }

    private void Update()
    {
        if (invulnerab)
        {
            invulnerableCounter -= Time.deltaTime;
            if (invulnerableCounter <= 0)
            {
                invulnerab = false;
            }
        }
    }

    //受伤计算
    public void TakeDamage(Attack attack)
    {
        //如果是无敌状态，退出
        if (invulnerab)
        {
            return;
        }

        if ((currentHealth - attack.damage) > 0)
        {
            //当前血量减去伤害
            currentHealth -= attack.damage;
            //执行受伤动画
            onTakeDamage?.Invoke(attack.transform);
            TriggerInvulnerable();
            
        }
        else
        {
            currentHealth = 0;
            //触发死亡
            OnDie.Invoke();
        }
        //更新UI
        OnHealthChange?.Invoke(this);
    }
    
    //触发无敌时间
    private void TriggerInvulnerable()
    {
        if (!invulnerab)
        {
            invulnerab = true;
            invulnerableCounter = invulnerableDuration;
        }
    }
}