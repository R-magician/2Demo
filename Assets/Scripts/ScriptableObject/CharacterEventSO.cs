//创建有参数的发布/订阅
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Event/CharacterEventSO")]
public class CharacterEventSO : ScriptableObject
{
    //事件的订阅--可以被任何一个代码订阅-
    public UnityAction<Character> OnEventRaised;

    //事件的调用
    public void RaisedEvent(Character character)
    {
        //广播的时候可以被所有订阅的函数都会执行对应的订阅事件
        OnEventRaised?.Invoke(character);
    }
}
