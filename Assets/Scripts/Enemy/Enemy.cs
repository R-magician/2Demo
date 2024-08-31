using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected Animator anim; 
    
    [Header("基本参数")]
    //基本的移动速度
    public float normalSpeed;
    //追击速度
    public float chaseSpeed;
    //当前的速度
    public float currentSpeed;
    //移动的方向
    public Vector3 faceDir;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentSpeed = normalSpeed;
    }

    private void Update()
    {
        faceDir = new Vector3(-transform.lossyScale.x, 0, 0);
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
}
