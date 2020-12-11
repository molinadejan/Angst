using System.Collections;
using UnityEngine;

// 맵 상에서 개별 사운드 출력을 위한 클래스 입니다.

public class SoundEvent : MonoBehaviour, IEvent
{
    public bool loop;
    public string clipName;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        audioSource.clip = AudioManager.instance.GetClip(clipName);
        audioSource.loop = loop;
    }

    public void EventPlay(float t)
    {
        StartCoroutine(EventPlayCor(t));
    }

    public void EventStop(float t)
    {
        StartCoroutine(EventStopCor(t));
    }

    public IEnumerator EventPlayCor(float t)
    {
        yield return new WaitForSeconds(t);

        if (audioSource != null && audioSource.clip != null)
            audioSource.Play();
    }

    public IEnumerator EventStopCor(float t)
    {
        yield return new WaitForSeconds(t);

        if (audioSource != null && audioSource.clip != null)
            audioSource.Stop();
    }
}
