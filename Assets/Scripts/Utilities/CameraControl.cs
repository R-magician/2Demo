//相机控制脚本

using System;
using UnityEngine;
//获取到相机命名空间
using Cinemachine;

public class CameraControl : MonoBehaviour
{
    //获取相机跟随最大范围
    private CinemachineConfiner2D confiner2D;
    //获取冲击
    public CinemachineImpulseSource impulseSource;

    //监听发起广播的事件
    public VoidEventSO camearShakeEvent;
    
    private void Awake()
    {
        confiner2D = GetComponent<CinemachineConfiner2D>();
    }

    private void OnEnable()
    {
        camearShakeEvent.OnEventRaised += OnCamerShakeEvent;
    }

    private void OnDisable()
    {
        camearShakeEvent.OnEventRaised -= OnCamerShakeEvent;
    }

    //监听订阅
    private void OnCamerShakeEvent()
    {
        //播放摄像机的震动
        impulseSource.GenerateImpulse();
    }

    //场景切换后更改
    private void Start()
    {
        GetNewCameraBounds();
    }

    //获取所有场景中的相机最大移动范围
    private void GetNewCameraBounds()
    {
        //获取到界限
        var obj = GameObject.FindGameObjectWithTag("Bounds");
        if (obj == null)
        {
            return;
        }

        //Collider2D
        confiner2D.m_BoundingShape2D = obj.GetComponent<Collider2D>();
        
        //切换场景后需要调方法清除缓存
        confiner2D.InvalidateCache();
    } 
}
