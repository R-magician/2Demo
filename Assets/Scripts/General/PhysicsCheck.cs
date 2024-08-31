using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsCheck : MonoBehaviour
{
    private CapsuleCollider2D coll;
    
    [Header("检测参数")] 
    //是否手动检测
    public bool manual;
    //脚底位移插值
    public Vector2 bottomOffset;
    //左侧偏移
    public Vector2 leftOffset;
    //右侧偏移
    public Vector2 rightOffset;
    //检测范围
    public float checkRaduis;
    //碰撞到那一个layer
    public LayerMask groundLayer;
    
    [Header("状态")]
    //是触碰到地面
    public bool isGround;
    //撞左墙
    public bool touchLeftWall;
    //撞右墙
    public bool touchRightWall;

    private void Awake()
    {
        coll = GetComponent<CapsuleCollider2D>();
        if (!manual)
        {
            //自动的，非手动
            //bounds-世界坐标碰撞体的范围
            rightOffset = new Vector2((coll.bounds.size.x + coll.offset.x) / 2, coll.bounds.size.y / 2);
            leftOffset = new Vector2(-rightOffset.x, rightOffset.y);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Check();
    }
    
    //检查碰撞体
    public void Check()
    {
        //检测地面状态
        isGround = Physics2D.OverlapCircle((Vector2)transform.position + new Vector2(bottomOffset.x * transform.localScale.x,bottomOffset.y), checkRaduis, groundLayer);
        
        //墙体的判断
        touchLeftWall = Physics2D.OverlapCircle((Vector2)transform.position + new Vector2(leftOffset.x,leftOffset.y) , checkRaduis, groundLayer);
        touchRightWall = Physics2D.OverlapCircle((Vector2)transform.position + new Vector2(rightOffset.x,rightOffset.y) , checkRaduis, groundLayer);
        
    }

    private void OnDrawGizmosSelected()
    {
        //来可视化这个技能的影响范围，确保其效果符合预期--绘制地板检测范围
        Gizmos.DrawWireSphere((Vector2)transform.position + new Vector2(bottomOffset.x * transform.localScale.x,bottomOffset.y) ,checkRaduis);
        //绘制左侧检测范围
        Gizmos.DrawWireSphere((Vector2)transform.position + new Vector2(leftOffset.x,leftOffset.y) ,checkRaduis);
        //绘制右侧检测范围
        Gizmos.DrawWireSphere((Vector2)transform.position + new Vector2(rightOffset.x,rightOffset.y)  ,checkRaduis);
    }
}
