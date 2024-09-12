//UI 管理
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //获取到血量物体
    public PlayerStatBar playerStatBar;
    
    [Header("事件监听")]
    public CharacterEventSO healthEvent;
    //卸载场景事件监听
    public SceneLoadEventSO unloadedSceneEvent;
    //加载场景事件监听
    public VoidEventSO loadDataEvent;
    //游戏结束监听
    public VoidEventSO gameOverEvent;
    //返回首页监听
    public VoidEventSO backToMainEvent;
    //同步音量值监听
    public FloatEventSO syncVolumeEvent;

    [Header("广播")]
    //游戏暂停启动
    public VoidEventSO pauseEvent;

    [Header("组件")]
    //面板组件
    public GameObject gameOverPanel;
    //重新开始
    public GameObject restartBtn;
    //获取设置按钮
    public Button settingBtn;
    //获取暂停面板 
    public GameObject pausePanel;
    //获取滑动条
    public Slider slider;

    private void Awake()
    {
        //给按钮添加监听事件
        settingBtn.onClick.AddListener(() =>
        {
            //检测暂停面板是否是激活状态
            if (pausePanel.activeInHierarchy)
            {
                pausePanel.SetActive(false);
                //游戏正常运行
                Time.timeScale = 1;
            }
            else
            {
                //发起广播
                pauseEvent.RaiseEvent();
                //开启面板
                pausePanel.SetActive(true);
                //游戏暂停
                Time.timeScale = 0;
            }
        });
    }

    private void OnEnable()
    {
        //受伤事件监听    
        healthEvent.OnEventRaised += OnHealthEvent;
        //卸载场景监听
        unloadedSceneEvent.LoadRequestEvent += OnUnLoadEvent;
        //加载场景监听
        loadDataEvent.OnEventRaised+=OnLoadEvent;
        //游戏结束监听
        gameOverEvent.OnEventRaised += OnGameOverEvent;
        //返回首页监听
        backToMainEvent.OnEventRaised += OnLoadEvent;
        //同步音量值监听
        syncVolumeEvent.OnEventRaised += OnSyncVolumeEvent;
    }

    private void OnDisable()
    {
        //取消事件
        healthEvent.OnEventRaised -= OnHealthEvent;
        unloadedSceneEvent.LoadRequestEvent -= OnUnLoadEvent;
        loadDataEvent.OnEventRaised -=OnLoadEvent;
        gameOverEvent.OnEventRaised -= OnGameOverEvent;
        backToMainEvent.OnEventRaised -= OnLoadEvent;
        syncVolumeEvent.OnEventRaised -= OnSyncVolumeEvent;
    }

    //同步音量
    private void OnSyncVolumeEvent(float arg0)
    {
        slider.value = (arg0 + 80)/100;
    }

    private void OnGameOverEvent()
    {
        //激活GameOver面板
        gameOverPanel.SetActive(true);
        //因为事件系统只会有一个，设置打开面板激活的按钮
        EventSystem.current.SetSelectedGameObject(restartBtn);
    }

    private void OnLoadEvent()
    {
        //关闭GameOver面板
        gameOverPanel.SetActive(false);
    }

    //显示血条
    private void OnUnLoadEvent(GameSceneSO arg0, Vector3 arg1, bool arg2)
    {
        var isMenu = arg0.sceneType == SceneType.Menu;
        playerStatBar.gameObject.SetActive(!isMenu);
    }

    //受伤事件
    private void OnHealthEvent(Character character)
    {
        //受伤百分比
        var persentage = character.currentHealth / character.maxHealth;
        //传递血量百分比
        playerStatBar.OnHealthChange(persentage);
    }
}
