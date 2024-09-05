using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Rigidbody2D rb;
    [HideInInspector] public Animator anim;
    [HideInInspector] public PhysicsCheck physicsCheck;

    [Header("基本参数")]
    //基本的移动速度
    public float normalSpeed;

    //追击速度
    public float chaseSpeed;

    //当前的速度
    [HideInInspector] public float currentSpeed;

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

    //当前状态
    protected BaseState currentState;

    //巡逻状态
    protected BaseState patrolState;

    //追击状态
    protected BaseState chaseState;

    //父类里面写一个虚拟的，字类里面复写
    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        physicsCheck = GetComponent<PhysicsCheck>();
        currentSpeed = normalSpeed;
        waitTimeConter = waitTime;
    }

    //启用时调用
    private void OnEnable()
    {
        currentState = patrolState;
        currentState.OnEnter(this);
    }

    private void Update()
    {
        //获取移动的方向
        faceDir = new Vector3(-transform.localScale.x, 0, 0);
        
        currentState.LogicUpdate();
        //延时转身
        TimeCounter();
    }

    private void FixedUpdate()
    {
        //没有受伤死亡和等待才移动
        if (!isHurt && !isDead && !wait)
        {
            Move();
        }

        currentState.PhysicsUpdate();
    }

    private void OnDisable()
    {
        currentState.OnExit();
    }

    //移动
    public virtual void Move()
    {
        rb.linearVelocity = new Vector2(currentSpeed * faceDir.x * Time.deltaTime, rb.linearVelocity.y);
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
        anim.SetBool("dead", true);
        isDead = true;
    }

    //执行销毁
    public void DestroyAfterAnimation()
    {
        //销毁这个物品
        Destroy(this.gameObject);
    }
}