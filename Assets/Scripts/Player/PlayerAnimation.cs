using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    private PhysicsCheck physicsCheck;
    private PlayerController playerController;

    private void Awake()
    {
        //获取到引用组件
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        physicsCheck = GetComponent<PhysicsCheck>();
        playerController = GetComponent<PlayerController>();
    }

    private void Update()
    {
        SetAnimator();
    }

    //动画切换--外部参数传入到动画器里面的参数-实时
    public void SetAnimator()
    {
        anim.SetFloat("velocityX",MathF.Abs(rb.velocity.x));
        anim.SetFloat("velocityY",rb.velocity.y);
        anim.SetBool("isGround",physicsCheck.isGround);
        anim.SetBool("isDead",playerController.isDead);
        anim.SetBool("isAttack",playerController.isAttack);
    }

    //人物受伤--自动改变
    public void PlayHurt()
    {
        anim.SetTrigger("hurt");
    }

    //人物攻击--自动改变
    public void PlayerAttack()
    {
        anim.SetTrigger("attack");
    }
}
