using UnityEngine;
using DG.Tweening;
using System.Collections;

// 문 열고 닫는 이벤트 정의
public class DoorControl : MonoBehaviour, IEvent
{
    public string doorOpenClip;
    public string doorCloseClip;

    public float doorOpenTime;
    public float doorCloseTime;

    public Ease ease;

    public float openAngle = -90;

    private AudioSource audioSource;
    private Animator anim;
    private bool isOpen;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        isOpen = transform.localEulerAngles.y != 0;
    }

    private void DoorOpen()
    {
        audioSource.clip = AudioManager.instance.GetClip(doorOpenClip);

        transform.DOLocalRotate(Vector3.up * openAngle, doorOpenTime)
            .OnStart(() =>
            {
                audioSource.Play();
            })
            .OnComplete(() => 
            { 
                isOpen = true; 
            });
    }

    private void DoorClose()
    {
        audioSource.clip = AudioManager.instance.GetClip(doorCloseClip);

        transform.DOLocalRotate(Vector3.zero, doorCloseTime).SetEase(ease)
            .OnComplete(() => 
            {
                isOpen = false;
                audioSource.Play();
            });
    }

    public void EventPlay(float t)
    {
        StartCoroutine(EventPlayCor(t));
    }

    public IEnumerator EventPlayCor(float t)
    {
        yield return new WaitForSeconds(t);

        if (isOpen) DoorClose();
        else DoorOpen();
    }

    public void EventStop(float t)
    {

    }

    public IEnumerator EventStopCor(float t)
    {
        yield return new WaitForSeconds(t);
    }
}
