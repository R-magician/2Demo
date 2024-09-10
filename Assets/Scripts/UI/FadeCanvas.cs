//场景的淡入淡出
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FadeCanvas : MonoBehaviour
{
    //遮挡图片
    public Image fadeImage;

    //执行渐变
    private void OnFadeEvent(Color target,float duration)
    {
        fadeImage.DOBlendableColor(target, duration);
    }
    
}
