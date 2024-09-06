//攻击
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//攻击属性及触发伤害
public class Attack : MonoBehaviour
{
    //伤害值
    public int damage;
    //攻击范围
    public float attackRange;
    //攻击频率
    public float attackRate;

    //当对象在触发区域内停留时，每帧调用一次
    private void OnTriggerStay2D(Collider2D other)
    {
        //访问被攻击的那个人--传入当前类
        other.GetComponent<Character>()?.TakeDamage(this);
    }
}
