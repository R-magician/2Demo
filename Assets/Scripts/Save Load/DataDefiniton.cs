//数据描述定义，生成GUID

using System;
using UnityEngine;

public class DataDefiniton : MonoBehaviour
{
    //类型约束
    public PersistentType persistentType;
    
    public string ID;

    private void OnValidate()
    {
        if (persistentType == PersistentType.ReadWrite)
        {
            //生成一个新的
            if (ID == string.Empty)
            {
                ID = System.Guid.NewGuid().ToString();
            }
        }
        else
        {
            //清空ID
            ID = string.Empty;
        }
        
    }
}
