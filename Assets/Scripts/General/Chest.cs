using System;
using UnityEngine;

public class Chest : MonoBehaviour,IInteractable
{
    private SpriteRenderer spriteRenderer;
    //切换图片--打开
    public Sprite openSprite;
    //切换图片--关闭
    public Sprite closeSprite;
    //所有的互动物品都有一个状态
    public bool isDone;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        spriteRenderer.sprite = isDone ? openSprite :closeSprite;
    }

    public void TriggerAction()
    {
        Debug.Log("Open Chest!");
        if (!isDone)
        {
            OpenChest();
        }
    }

    //打开宝箱
    public void OpenChest()
    {
        spriteRenderer.sprite = openSprite;
        //宝箱标记被打开
        isDone = true;
        //把标签切换成Untagged
        this.gameObject.tag = "Untagged";
    }
}
