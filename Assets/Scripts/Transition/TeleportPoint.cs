//传送控制脚本
using UnityEngine;

public class TeleportPoint : MonoBehaviour,IInteractable
{
    //记录下一个场景的坐标
    public Vector3 positionToGo;
    public void TriggerAction()
    {
        Debug.Log("传送");
    }
}
