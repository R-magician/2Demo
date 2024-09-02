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

    //受伤的力
    public float hurtForce;

    //攻击对象
    public Transform attacker;

    [Header("计时器")]
    //等待时间
    public float waitTime;

    //
    public float waitTimeConter;

    //等待状态
    public bool wait;

    [Header("状态")]
    //受伤
    public bool isHurt;
    //死亡
    public bool isDead;

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
        if ((physicsCheck.touchLeftWall && faceDir.x < 0) || (physicsCheck.touchRightWall && faceDir.x > 0))
        {
            wait = true;
            //关闭走路，开始等待
            anim.SetBool("walk", false);
        }

        //延时转身
        TimeCounter();
    }

    private void FixedUpdate()
    {
        if (!isHurt && !isDead)
        {
            Move();
        }
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

    //受到攻击
    public void OnTakeDamage(Transform attackTrans)
    {
        attacker = attackTrans;
        //被攻击的对象可以转身
        if ((attackTrans.position.x - transform.position.x) > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        if ((attackTrans.position.x - transform.position.x) < 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        //受伤
        isHurt = true;
        anim.SetTrigger("hurt");
        //击退方向
        Vector2 dir = new Vector2(transform.position.x - attackTrans.position.x, 0).normalized;
        
        //开启携程
        StartCoroutine(OnHurt(dir));
    }

    //使用携程的方式使攻击后重新走起来--返回一个迭代器
    private IEnumerator OnHurt(Vector2 dir)
    {
        //添加力,Impulse--冲击力
        rb.AddForce(dir * hurtForce, ForceMode2D.Impulse);
        //等待条件
        yield return new WaitForSeconds(0.5f);
        //受伤关闭
        isHurt = false;
    }

    //死亡
    public void onDie()
    {
        //改变图层为ignore
        gameObject.layer = 2;
        anim.SetBool("dead",true);
        isDead = true;
    }
    
    //执行销毁
    public void DestroyAfterAnimation()
    {
        //销毁这个物品
        Destroy(this.gameObject);  
    }
}