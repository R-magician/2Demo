//传送控制脚本
using UnityEngine;

public class TeleportPoint : MonoBehaviour,IInteractable
{
    //添加事件
    public SceneLoadEventSO LoadEventSo;
    //要去到那一个场景
    public GameSceneSO sceneToGO;
    //记录下一个场景的坐标
    public Vector3 positionToGo;
    public void TriggerAction()
    {
        Debug.Log("传送");
        //呼叫(广播)一下事件
        LoadEventSo.RaiseLoadRequestEvent(sceneToGO,positionToGo,true);
    }
}
