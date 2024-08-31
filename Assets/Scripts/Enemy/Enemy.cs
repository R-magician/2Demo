using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected Animator anim;
    private PhysicsCheck physicsCheck;
    
    [Header("基本参数")]
    //基本的移动速度
    public float normalSpeed;
    //追击速度
    public float chaseSpeed;
    //当前的速度
    public float currentSpeed;
    //移动的方向
    public Vector3 faceDir;
    
    [Header("计时器")]
    //等待时间
    public float waitTime;
    //
    public float waitTimeConter;
    //等待状态
    public bool wait;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        physicsCheck = GetComponent<PhysicsCheck>();
        currentSpeed = normalSpeed;
        waitTimeConter = waitTime;
    }

    private void Update()
    {
        //获取移动的方向
        faceDir = new Vector3(-transform.localScale.x, 0, 0);

        //如果碰到两侧的墙，角色翻转
        if ((physicsCheck.touchLeftWall && faceDir.x < 0) || (physicsCheck.touchRightWall && faceDir.x>0))
        {
            wait = true;
            //关闭走路，开始等待
            anim.SetBool("walk",false);
        }

        //延时转身
        TimeCounter();
    }

    private void FixedUpdate()
    {
        Move();
    }

    //移动
    public virtual void Move()
    {
        rb.velocity = new Vector2(currentSpeed * faceDir.x * Time.deltaTime, rb.velocity.y);
    }
    
    //计时器
    public void TimeCounter()
    {
        if (wait)
        {
            waitTimeConter -= Time.deltaTime;
            if (waitTimeConter <= 0)
            {
                wait = false;
                waitTimeConter = waitTime;
                transform.localScale = new Vector3(faceDir.x, 1, 1);
            }
        }
    }
}
