//生成打包--第一个场景文件

using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class InitalLoad : MonoBehaviour
{
    //加载的场景
    public AssetReference persistentScene;

    private void Awake()
    {
        Addressables.LoadSceneAsync(persistentScene);
    }
}
