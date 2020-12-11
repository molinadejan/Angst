/*
using UnityEngine;
using DG.Tweening;
using System.Collections;

public class ObjectBreathing : MonoBehaviour
{
    public Ease myEase = Ease.InBounce;

    private float startScale;
    private float changeScale;
    private float curScale;

    private float changeTime;

    private AudioSource audioSource;

    private void Awake()
    {
        startScale = transform.localScale.x;
        changeScale = startScale * 1.1f;
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        curScale = changeScale;
        changeTime = Random.Range(0.5f, 3f);
        ChangeScale(curScale);

        StartCoroutine(StartAudio());
    }

    private IEnumerator StartAudio()
    {
        yield return new WaitForSeconds(Random.Range(0f, 2f));
        audioSource.Play();
    }

    private void ChangeScale(float f)
    {
        transform.DOScale(Vector3.one * f, changeTime)
            .SetEase(myEase)
            .OnComplete(() =>
            {
                if(curScale == changeScale)
                {
                    curScale = startScale;
                }
                else
                {
                    curScale = changeScale;
                }

                ChangeScale(curScale);
            });
    }
}
*/