// 传递音频文件的事件
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Event/PlayAudioEventSO")]
public class PlayAudioEventSO : ScriptableObject
{
    public UnityAction<AudioClip> OnEventRaised;

    public void OnRaised(AudioClip clip)
    {
        OnEventRaised?.Invoke(clip);
    }
}
