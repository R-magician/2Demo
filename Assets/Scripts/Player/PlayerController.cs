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
        Move();
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
}
