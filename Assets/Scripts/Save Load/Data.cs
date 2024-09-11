//保存的数据类

using System.Collections.Generic;
using UnityEngine;

public class Data
{
   //场景数据
   public string sceneToSave;
   //物体向量数据
   public Dictionary<string, Vector3> characterPosDict = new Dictionary<string, Vector3>();
   //物体的float类型数据（血量，power值）
   public Dictionary<string, float> floatSaveData = new Dictionary<string, float>();
   
   //场景数据保存
   public void SaveGameScene(GameSceneSO savedSceneSo)
   {
      //将Object类型的数据序列化成JSON数据
      sceneToSave = JsonUtility.ToJson(savedSceneSo);
      Debug.Log(sceneToSave);
   }

   //获取场景数据
   public GameSceneSO GetSaveScene()
   {
      //创建一个SO文件
      var newScene = ScriptableObject.CreateInstance<GameSceneSO>();
      //将JSON数据保存为Object数据---将sceneToSave数据覆盖给newScene
      JsonUtility.FromJsonOverwrite(sceneToSave,newScene);

      return newScene;
   }
}
