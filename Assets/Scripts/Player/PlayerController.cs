using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public PlayerInputControl inputControl;

    //比Start()先执行
    private void Awake()
    {
        //创建一个实例
        inputControl = new PlayerInputControl();
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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
