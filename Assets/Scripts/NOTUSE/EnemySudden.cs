/*
using UnityEngine;
using DG.Tweening;
using System.Collections;

public class EnemySudden : EnemyBasic, IEvent
{
    public float moveTime;
    public float maxSize;

    public Ease spriteEase;
    public Ease moveEase;

    public string clipName;

    private void Start()
    {
        sr.color = new Color(1, 1, 1, 0);
        audioSource.clip = AudioManager.instance.GetClip(clipName);
    }

    private void Update()
    {
        Vector3 target = transform.position - PlayerControl.instance.cams.transform.position;
        transform.rotation = Quaternion.LookRotation(target);
    }

    public void EventPlay(float t)
    {
        StartCoroutine(EventPlayCor(t));
    }

    public IEnumerator EventPlayCor(float t)
    {
        //float waitTime = t >= audioSource.clip.length ? 0 : audioSource.clip.length - t;

        yield return new WaitForSeconds(t);

        //audioSource.Play();

        float waitTime = 2f;

        sr.DOColor(new Color(0.6f, 0.6f, 0.6f, 0.6f), moveTime).SetEase(spriteEase);
        sr.transform.DOScale(new Vector3(1, 1, 0) * maxSize, moveTime);
        transform.DOMove(PlayerControl.instance.cams.transform.position, moveTime).SetEase(moveEase).OnComplete(() => sr.color = new Color(0, 0, 0, 0));

        yield return new WaitForSeconds(waitTime);

        gameObject.SetActive(false);
    }

    public void EventStop(float t)
    {
        throw new System.NotImplementedException();
    }

    public IEnumerator EventStopCor(float t)
    {
        throw new System.NotImplementedException();
    }
}
*/
