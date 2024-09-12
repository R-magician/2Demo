//音频管理

using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [Header("事件的监听")]
    public PlayAudioEventSO FXEvent;
    public PlayAudioEventSO BGMEvent;
    //滑动条事件监听
    public FloatEventSO volumeEvent;
    //暂停监听
    public VoidEventSO pauseEvent;

    [Header("广播")]
    //同步音频声量
    public FloatEventSO syncVolumeEvent;
    
    [Header("组件")]
    //Bgm
    public AudioSource BGMSource;
    //其他音效
    public AudioSource FXSource;
    //获取音频混合器
    public AudioMixer mixer;
    

    private void OnEnable()
    {
        //添加监听
        FXEvent.OnEventRaised += OnFXEvent;
        BGMEvent.OnEventRaised += OnBGMEvent;
        volumeEvent.OnEventRaised += OnVolumeEvent;
        pauseEvent.OnEventRaised += OnPauseEvent;
    }

    private void OnDisable()
    {
        //取消监听
        FXEvent.OnEventRaised -= OnFXEvent;
        BGMEvent.OnEventRaised -= OnBGMEvent;
        volumeEvent.OnEventRaised -= OnVolumeEvent;
        pauseEvent.OnEventRaised -= OnPauseEvent;

    }

    private void OnPauseEvent()
    {
        float m;
        //把值输出到m中
        mixer.GetFloat("MasterVolume",out m);
        Debug.Log(m);
        //广播
        syncVolumeEvent.RaiseEvent(m);
    }

    private void OnVolumeEvent(float arg0)
    {
        //设置音频混合器音量
        mixer.SetFloat("MasterVolume", arg0 * 100 - 80);
    }

    private void OnBGMEvent(AudioClip clip)
    {
        BGMSource.clip = clip;
        BGMSource.Play();
    }

    private void OnFXEvent(AudioClip clip)
    {
        FXSource.clip = clip;
        FXSource.Play();
    }
}
