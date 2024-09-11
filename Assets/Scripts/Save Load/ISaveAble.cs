//存储接口
using UnityEngine;

public interface ISaveAble
{
    //一定要获取到GUID
    DataDefiniton GetDataID();
    //注册到管理类
    void RegisterSaveData()
    {
        DataManager.instance.RegisterSaveData(this);
    }
    //注销物体
    void UnRegisterSaveData()=>DataManager.instance.UnRegisterSaveData(this);
    //保存的数据
    void SetSaveData(Data data);
    //加载数据
    void LoadData(Data data);
}
