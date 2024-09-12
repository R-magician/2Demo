//传递Float参数的事件
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Event/FloatEventSO")]
public class FloatEventSO : ScriptableObject
{
    //订阅
    public UnityAction<float> OnEventRaised;

    //在其他代码中一直发起监听
    public void RaiseEvent(float f)
    {
        OnEventRaised.Invoke(f);
    }
}
