//玩家控制
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("监听事件")]
    //场景卸载监听事件
    public SceneLoadEventSO LoadEventSo;
    //场景加载之后的监听事件
    public VoidEventSO afterSceneLoadEvent;
    
    
    public PlayerInputControl inputControl;

    public Vector2 inputDirection;

    public float speed;
    public float jumpFore;
    //受伤反弹的一个力
    public float hurtForce;

    private Rigidbody2D rb;
    private PhysicsCheck physicsCheck;
    private PlayerAnimation playerAnimation;
    private CapsuleCollider2D coll;
    
    [Header("物理材质")]
    //有摩擦力的材质--走路
    public PhysicsMaterial2D normal;
    //上墙的材质--光滑
    public PhysicsMaterial2D wall;
    
    [Header("状态")]

    //受伤状态
    public bool isHurt;

    //死亡状态
    public bool isDead;

    //攻击状态
    public bool isAttack;

    //比Start()先执行
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        physicsCheck = GetComponent<PhysicsCheck>();
        playerAnimation = GetComponent<PlayerAnimation>();
        coll = GetComponent<CapsuleCollider2D>();
        //创建一个实例
        inputControl = new PlayerInputControl();
        //跳跃-按下按钮开始触发--started
        inputControl.GamePlay.Jump.started += Jump;

        //攻击
        inputControl.GamePlay.Attack.started += PlayerAttack;
    }

    //当组件被启动的时候
    private void OnEnable()
    {
        //启用输入系统
        inputControl.Enable();
        LoadEventSo.LoadRequestEvent += OnLoadEvent;
        afterSceneLoadEvent.OnEventRaised += OnAfterSceneLoad;
    }

    //当组件被关闭的时候
    private void OnDisable()
    {
        inputControl.Disable();
        LoadEventSo.LoadRequestEvent -= OnLoadEvent;
        afterSceneLoadEvent.OnEventRaised -= OnAfterSceneLoad;

    }

    // Update is called once per frame
    void Update()
    {
        inputDirection = inputControl.GamePlay.Move.ReadValue<Vector2>();
        CheckState();
    }

    private void FixedUpdate()
    {
        if (!isHurt && !isAttack)
        {
            Move();
        }
    }
    
    //卸载场景触发
    private void OnLoadEvent(GameSceneSO arg0, Vector3 arg1, bool arg2)
    {
        //停止角色的控制
        inputControl.GamePlay.Disable();
    }
    
    //加载场景触发
    private void OnAfterSceneLoad()
    {
        //开始角色控制
        inputControl.GamePlay.Enable();
    }

    public void Move()
    {
        rb.linearVelocity = new Vector2(inputDirection.x * speed * Time.deltaTime, rb.linearVelocity.y);

        int faceDir = (int)transform.localScale.x;
        if (inputDirection.x > 0)
        {
            faceDir = 1;
        }

        if (inputDirection.x < 0)
        {
            faceDir = -1;
        }

        //人物翻转
        transform.localScale = new Vector3(faceDir, 1, 1);
    }

    //跳跃
    private void Jump(InputAction.CallbackContext obj)
    {
        if (physicsCheck.isGround)
        {
            rb.AddForce(transform.up * jumpFore, ForceMode2D.Impulse);
            GetComponent<AudioDefination>().PlayAudioClip();
        }
    }

    //攻击
    private void PlayerAttack(InputAction.CallbackContext obj)
    {
        //不在地面上，不能攻击
        if (physicsCheck.isGround)
        {
            //改变动画器里面的参数值
            playerAnimation.PlayerAttack();
            isAttack = true;
        }
        
    }

    #region UnityEvent里面触发的

    //受伤时
    public void GetHurt(Transform attacker)
    {
        isHurt = true;
        //降速
        rb.linearVelocity = Vector2.zero;
        //反弹(人物的坐标-攻击者的坐标)
        Vector2 dir = new Vector2((transform.position.x - attacker.position.x), 0).normalized;

        //添加一个顺时的力
        rb.AddForce(dir * hurtForce, ForceMode2D.Impulse);
    }

    //
    public void PlayerDead()
    {
        isDead = true;
        //关掉输入系统
        inputControl.GamePlay.Disable();
    }

    #endregion

    private void CheckState()
    {
        coll.sharedMaterial = physicsCheck.isGround ? normal : wall;
    }
}