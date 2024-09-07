//没有参数的发布订阅
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName="Event/VoidEventSO")]
public class VoidEventSO : ScriptableObject
{
    //订阅
    public UnityAction OnEventRaised;

    //在其他代码中一直发起监听
    public void RaiseEvent()
    {
        OnEventRaised.Invoke();
    }
}
