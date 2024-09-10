//传递场景加载参数事件
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Event/SceneLoadEventSO")]
public class SceneLoadEventSO : ScriptableObject
{
    //创建事件
    public UnityAction<GameSceneSO, Vector3, bool> LoadRequestEvent;
    
    /// <summary>
    /// 场景加载请求
    /// </summary>
    /// <param name="locationToLoad">要去的场景</param>
    /// <param name="posToGo">player的目的坐标</param>
    /// <param name="fadeScreen">是否渐入渐出</param>
    //启动的方法
    public void RaiseLoadRequestEvent(GameSceneSO locationToLoad, Vector3 posToGo, bool fadeScreen)
    {
        //呼叫一下事件
        LoadRequestEvent.Invoke(locationToLoad,posToGo,fadeScreen);
    }
}
