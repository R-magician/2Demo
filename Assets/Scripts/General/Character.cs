using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void Start()
    {
        currentHealth = maxHealth;
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
            TriggerInvulnerable();
        }
        else
        {
            currentHealth = 0;
            //触发死亡
        }
       
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