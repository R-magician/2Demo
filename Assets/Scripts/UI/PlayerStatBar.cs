using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatBar : MonoBehaviour
{
    //绿色血量
    public Image healthImge;
    //红色血量
    public Image healthDelayImge;
    //能量
    public Image PowerImage;

    private void Update()
    {
        //达到减血的效果
        if (healthDelayImge.fillAmount > healthImge.fillAmount)
        {
            //红色比绿色多
            healthDelayImge.fillAmount -= Time.deltaTime;
        }
    }

    //血量变更--persentage:接收血量变更百分比
    public void OnHealthChange(float persentage)
    {
        healthImge.fillAmount = persentage;
    }
}
