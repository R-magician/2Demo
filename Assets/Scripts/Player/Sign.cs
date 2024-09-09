//场景互动控制脚本

using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.DualShock;//ps设备的命名空间

public class Sign : MonoBehaviour
{
    private PlayerInputControl playerInput;
    //不同设备播放的动画
    private Animator anim;

    public Transform playerTrans;
    //获取子物体
    public GameObject signSprite;
    //子物体是否激活
    private bool canPress;

    private void Awake()
    {
        //获取子物体的动画控制器
        anim = signSprite.GetComponent<Animator>();
        playerInput = new PlayerInputControl();
        playerInput.Enable();
    }

    private void OnEnable()
    {
        InputSystem.onActionChange += OnActionChange;
    }

    private void Update()
    {
        //显示子物体
        signSprite.GetComponent<SpriteRenderer>().enabled = canPress;
        signSprite.transform.localScale = playerTrans.localScale;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("interactable"))
        {
            canPress = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        canPress = false;
    }
    
    private void OnActionChange(object obj, InputActionChange actionChange)
    {
        //更具不同的设备切换不同的动画
        if (actionChange == InputActionChange.ActionStarted)
        {
            //获取设备按下的按键
            var d = ((InputAction)obj).activeControl.device;

            switch (d.device)
            {
                //键盘输入。播放对应的动画
                case Keyboard:
                    anim.Play("keyboard");
                    break;
                //PS手柄输入
                case DualShockGamepad:
                    anim.Play("ps");
                    break;
            }
        }
    }
}
