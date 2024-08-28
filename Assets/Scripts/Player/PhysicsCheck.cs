using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsCheck : MonoBehaviour
{
    [Header("检测参数")]
    //脚底位移插值
    public Vector2 bottomOffset;
    //检测范围
    public float checkRaduis;
    //碰撞到那一个layer
    public LayerMask groundLayer;
    
    [Header("状态")]
    //是触碰到地面
    public bool isGround;

    // Update is called once per frame
    void Update()
    {
        Check();
    }
    
    //检查碰撞体
    public void Check()
    {
        //检测地面状态
        isGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, checkRaduis, groundLayer);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset ,checkRaduis);
    }
}
