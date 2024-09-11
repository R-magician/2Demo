//存储点

using System;
using UnityEngine;

public class SavePoint : MonoBehaviour,IInteractable
{
    [Header("广播")]
    public VoidEventSO loadGameEvent;
    
    [Header("变量参数")]
    //设置子物体的精灵组件
    public SpriteRenderer spriteRenderer;
    //获取物体上的点光源
    public GameObject lightObj;
    //熄灭图片
    public Sprite darkImg;
    //亮起图片
    public Sprite lightImg;
    //是否交互过
    public bool isDone; 
    
    private void OnEnable()
    {
        spriteRenderer.sprite = isDone ? lightImg :darkImg;
        lightObj.SetActive(isDone);
    }

    public void TriggerAction()
    {
        if (!isDone)
        {
            isDone = true;
            spriteRenderer.sprite = lightImg;
            lightObj.SetActive(true);
            
            //TODO：保存数据
            loadGameEvent.RaiseEvent();
            
            //把标签切换成Untagged
            this.gameObject.tag = "Untagged";
        }
    }
}
