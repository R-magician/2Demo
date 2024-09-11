//玩家，敌人属性
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

//人物属性数值计算
public class Character : MonoBehaviour,ISaveAble
{
    [Header("监听事件")]
    //新的开始
    public VoidEventSO newGameEvent;
    
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

    //开始游戏点击触发
    private void NewGame()
    {
        currentHealth = maxHealth;
        //更新UI
        OnHealthChange.Invoke(this);
    }

    private void OnEnable()
    {
        newGameEvent.OnEventRaised += NewGame;
        ISaveAble saveAble = this;
        //无论组件挂在哪一个物体上都把自己传过去强制执行一下
        saveAble.RegisterSaveData();
    }

    private void OnDisable()
    {
        newGameEvent.OnEventRaised -= NewGame;
        ISaveAble saveAble = this;
        saveAble.UnRegisterSaveData();
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

    //开始触发碰撞器
    private void OnTriggerStay2D(Collider2D other)
    {
        //判断触发器是不是water标签
        if (other.CompareTag("water"))
        {
            currentHealth = 0;
            //死亡更新血量
            OnDie?.Invoke();
            //更新UI
            OnHealthChange?.Invoke(this);
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

    //获取GUID
    public DataDefiniton GetDataID()
    {
        return GetComponent<DataDefiniton>();
    }

    //保存数据
    public void SetSaveData(Data data)
    {
        if (data.characterPosDict.ContainsKey(GetDataID().ID))
        {
            //如果数据里面有这个ID
            data.characterPosDict[GetDataID().ID] = transform.position;
        }
        else
        {
            //第一次保存数据
            data.characterPosDict.Add(GetDataID().ID,transform.position);
        }
    }

    //加载数据
    public void LoadData(Data data)
    {
        if (data.characterPosDict.ContainsKey(GetDataID().ID))
        {
            //如果数据里面有这个ID-加载数据
            transform.position = data.characterPosDict[GetDataID().ID];
        } 
    }
}