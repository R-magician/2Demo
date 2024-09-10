//渐入渐出事件
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Event/FadeEventSO")]
public class FadeEventSO : ScriptableObject
{
   public UnityAction<Color, float, bool> OnEventRaised;
   
   /// <summary>
   /// 逐渐变黑
   /// </summary>
   /// <param name="duration"></param>
   public void FadeIn(float duration)
   {
      //启动事件订阅的函数方法
      RaiseEvent(Color.black, duration, true);
   }
   
   /// <summary>
   /// 逐渐变透明
   /// </summary>
   /// <param name="duration"></param>
   public void FadeOut(float duration)
   {
      RaiseEvent(Color.clear,duration,false);
   }

   public void RaiseEvent(Color target, float duration, bool fadeIn)
   {
      OnEventRaised?.Invoke(target,duration,fadeIn);
   }
}
