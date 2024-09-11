//数据存储和读取数据管理脚本

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-100)]
public class DataManager : MonoBehaviour
{
    //单例声明，游戏启动后会一直在内存中
    public static DataManager instance;

    [Header("事件监听")] 
    //保存事件监听
    public VoidEventSO saveDataEvent;

    private List<ISaveAble> saveAbleList = new List<ISaveAble>();

    private Data saveData;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            //销毁--确保有且只有一个
            Destroy(this.gameObject);
        }

        saveData = new Data();
    }

    private void OnEnable()
    {
        //监听保存
        saveDataEvent.OnEventRaised += Save;
    }

    private void Update()
    {
        //在这一帧按下L键的时候
        if (Keyboard.current.lKey.wasPressedThisFrame)
        {
            Load();
        }
    }

    private void OnDisable()
    {
        saveDataEvent.OnEventRaised -= Save;
    }

    //注册的函数方法
    public void RegisterSaveData(ISaveAble saveAble)
    {
        //判断传过来的saveAble是否包含在saveAbleList
        if (!saveAbleList.Contains(saveAble))
        {
            //没包含
            saveAbleList.Add(saveAble);
        }
    }
    
    //注销
    public void UnRegisterSaveData(ISaveAble saveAble)
    {
        saveAbleList.Remove(saveAble);
    }
    
    //保存
    public void Save()
    {
        foreach (var saveable in saveAbleList)
        {
            saveable.SetSaveData(saveData);
        }

        foreach (var item in saveData.characterPosDict)
        {
            Debug.Log(item.Key+"  "+item.Value);
        }
    }
    
    //加载
    public void Load()
    {
        foreach (var saveable in saveAbleList)
        {
            saveable.LoadData(saveData);
        }
    }
}
