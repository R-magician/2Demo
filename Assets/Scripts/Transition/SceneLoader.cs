//场景加载控制脚本

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [Header("事件的监听")]
    public SceneLoadEventSO LoadEventSo;
    //加载的第一个场景
    public GameSceneSO firstLoadScene;

    //当前加载的场景
    [SerializeField]private GameSceneSO currentLoadedScene;
    //要去到的场景
    private GameSceneSO sceneToLoad;
    //要去到场景的坐标
    private Vector3 positionTOGo;
    //是否渐隐渐显
    private bool fadeScreen;
    
    //获得渐隐渐出的等待时间
    public float fadeDuration;

    private void Awake()
    {
        //加载第一个场景
        //Addressables.LoadSceneAsync(firstLoadScene.sceneAsset, LoadSceneMode.Additive);
        currentLoadedScene = firstLoadScene;
        currentLoadedScene.sceneAsset.LoadSceneAsync(LoadSceneMode.Additive);
    }

    private void OnEnable()
    {
        LoadEventSo.LoadRequestEvent += OnLoadRequestEvent;
    }

    private void OnDisable()
    {
        LoadEventSo.LoadRequestEvent -= OnLoadRequestEvent;
    }

    private void OnLoadRequestEvent(GameSceneSO arg0, Vector3 arg1, bool arg2)
    {
        sceneToLoad = arg0;
        positionTOGo = arg1;
        fadeScreen = arg2;

        if (currentLoadedScene != null)
        {
            //执行携程
            StartCoroutine(UnLoadPreviousScene());
        }
        
    }

    //卸载当前的场景
    private IEnumerator UnLoadPreviousScene()
    {
        if (fadeScreen)
        {
            //实现渐隐渐显
        }

        yield return new WaitForSeconds(fadeDuration);
        //卸载场景
        yield return currentLoadedScene.sceneAsset.UnLoadScene();

        LoadNewScene();

    }

    //加载新的场景
    private void LoadNewScene()
    {
        sceneToLoad.sceneAsset.LoadSceneAsync(LoadSceneMode.Additive, true);
    }
    
}
