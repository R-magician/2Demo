//场景加载控制脚本

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    //player 的transform
    public Transform playerTrans;
    //初始坐标
    public Vector3 firstPosition;
    
    [Header("事件的监听")]
    public SceneLoadEventSO LoadEventSo;
    //加载的第一个场景
    public GameSceneSO firstLoadScene;
    
    [Header("广播切换场景")]
    public VoidEventSO afterSceneLoadedEvent;

    public FadeEventSO fadeEvent;
    
    //当前加载的场景
    [SerializeField]private GameSceneSO currentLoadedScene;
    //要去到的场景
    private GameSceneSO sceneToLoad;
    //要去到场景的坐标
    private Vector3 positionTOGo;
    //是否渐隐渐显
    private bool fadeScreen;
    //场景是否正在加载
    private bool isLoading;
    
    //获得渐隐渐出的等待时间
    public float fadeDuration;

    private void Awake()
    {
        //加载第一个场景
        //Addressables.LoadSceneAsync(firstLoadScene.sceneAsset, LoadSceneMode.Additive);
        // currentLoadedScene = firstLoadScene;
        // currentLoadedScene.sceneAsset.LoadSceneAsync(LoadSceneMode.Additive);
    }

    //TODU:做完MainMenu之后要改
    private void Start()
    {
        NewGame();
    }

    private void OnEnable()
    {
        LoadEventSo.LoadRequestEvent += OnLoadRequestEvent;
    }

    private void OnDisable()
    {
        LoadEventSo.LoadRequestEvent -= OnLoadRequestEvent;
    }

    private void NewGame()
    {
        sceneToLoad = firstLoadScene;
        OnLoadRequestEvent(sceneToLoad,firstPosition,true);
    }
    
    /// <summary>
    /// 场景加载事件请求
    /// </summary>
    /// <param name="arg0"></param>
    /// <param name="arg1"></param>
    /// <param name="arg2"></param>
    private void OnLoadRequestEvent(GameSceneSO arg0, Vector3 arg1, bool arg2)
    {
        //场景正在加载
        if (isLoading)
        {
            return;
        }
        isLoading = true;
        sceneToLoad = arg0;
        positionTOGo = arg1;
        fadeScreen = arg2;

        if (currentLoadedScene != null)
        {
            //执行携程
            StartCoroutine(UnLoadPreviousScene());
        }
        else
        {
            //加载新场景
            LoadNewScene();
        }
        
    }

    //卸载当前的场景
    private IEnumerator UnLoadPreviousScene()
    {
        if (fadeScreen)
        {
            //场景逐渐变黑
            fadeEvent.FadeIn(fadeDuration); 
        }

        yield return new WaitForSeconds(fadeDuration);
        //卸载场景
        yield return currentLoadedScene.sceneAsset.UnLoadScene();

        //关闭人物
        playerTrans.gameObject.SetActive(false);
        
        //加载新场景
        LoadNewScene();

    }

    //加载新的场景
    private void LoadNewScene()
    {
        var loadingOption =  sceneToLoad.sceneAsset.LoadSceneAsync(LoadSceneMode.Additive, true);
        //当场景加载好可以做的事件
        loadingOption.Completed += OnLoadCompleted;
    }

    /// <summary>
    /// 场景加载完成之后
    /// </summary>
    /// <param name="obj"></param>
    private void OnLoadCompleted(AsyncOperationHandle<SceneInstance> obj)
    {
        //存储当前场景
        currentLoadedScene = sceneToLoad;
        //赋予下一个场景player的位置
        playerTrans.position = positionTOGo;
        
        //场景加载完成后启动player
        playerTrans.gameObject.SetActive(true);
        
        if (fadeScreen)
        {
            //有渐入渐出的效果
        }

        //场景加载完成
        isLoading = false;
        //播报
        afterSceneLoadedEvent.RaiseEvent();
    }
}
