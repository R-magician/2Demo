// Addressable 场景信息
using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu( menuName = "Game Scene/GameSceneSO")]
public class GameSceneSO : ScriptableObject
{
   
   public SceneType sceneType;
   //AssetReference资源的引用--要先安装插件
   public AssetReference sceneAsset;
}
