using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public PlayerInputControl inputControl;
    
    public Vector2 inputDirection;

    public float speed;
    public float jumpFore;
    
    private Rigidbody2D rb;
    private PhysicsCheck physicsCheck;
    
    //受伤反弹的一个力
    public float hurtForce;
    //受伤状态
    public bool isHurt;
    //死亡状态
    public bool isDead;
    

    //比Start()先执行
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        physicsCheck = GetComponent<PhysicsCheck>();
        //创建一个实例
        inputControl = new PlayerInputControl();
        inputControl.GamePlay.Jump.started += Jump;
    }

    //当组件被启动的时候
    private void OnEnable()
    {
        //启用输入系统
        inputControl.Enable();
    }

    //当组件被关闭的时候
    private void OnDisable()
    {
        inputControl.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        inputDirection = inputControl.GamePlay.Move.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        if (!isHurt)
        {
            Move(); 
        }
    }

    public void Move()
    {
        rb.velocity = new Vector2(inputDirection.x * speed * Time.deltaTime, rb.velocity.y);

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
            rb.AddForce(transform.up * jumpFore,ForceMode2D.Impulse);
        }
    }

    //受伤时
    public void GetHurt(Transform attacker)
    {
        isHurt = true;
        //降速
        rb.velocity = Vector2.zero;
        //反弹(人物的坐标-攻击者的坐标)
        Vector2 dir = new Vector2((transform.position.x - attacker.position.x),0).normalized;
        
        //添加一个顺时的力
        rb.AddForce(dir * hurtForce,ForceMode2D.Impulse);
    }

    //
    public void PlayerDead()
    {
        isDead = true;
        //关掉输入系统
        inputControl.GamePlay.Disable();
    }
}
