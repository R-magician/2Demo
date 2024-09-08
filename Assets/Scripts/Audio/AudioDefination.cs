//音频自定义脚本

using System;
using UnityEngine;

public class AudioDefination : MonoBehaviour
{
    //通过事件的方式进行广播来传递
    public PlayAudioEventSO playAudioEvent;
    //音频的片段
    public AudioClip audioClip;
    //是否一开始就播放
    public bool playOnEnable;

    private void OnEnable()
    {
        if (playOnEnable)
        {
            PlayAudioClip();
        }
    }

    //播放音乐片段
    public void PlayAudioClip()
    {
        playAudioEvent.OnRaised(audioClip);
    }
}
