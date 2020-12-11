/*
using UnityEngine;
using DG.Tweening;
using System.Collections;

public class Hanging : EnemyBasic, IEvent
{
    public float attackPower;
    public float changeScale;
    private float scaleFloat = 1;

    private void Start()
    {
        ChangeScale(4.5f);
    }

    private void Update()
    {
        Vector3 target = transform.position - PlayerControl.instance.cams.transform.position;
        target.y = 0;
        transform.rotation = Quaternion.LookRotation(target);
    }

    private void ChangeScale(float t)
    {
        gameObject.transform.DOScaleX(scaleFloat, t)
            .OnComplete(() =>
            {
                if (scaleFloat == 1)
                {
                    scaleFloat = changeScale;
                    ChangeScale(3.5f);
                }
                else
                {
                    scaleFloat = 1;
                    ChangeScale(4.5f);
                }
            });
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

        audioSource.Stop();
        audioSource.loop = false;
        audioSource.clip = AudioManager.instance.GetClip("hangingAttack");
        audioSource.Play();

        PlayerStress.instance.Stress -= attackPower;
    }

    public IEnumerator EventStopCor(float t)
    {
        yield return new WaitForSeconds(t);

        audioSource.Stop();
        audioSource.loop = true;
        audioSource.clip = AudioManager.instance.GetClip("hangingStay");
        audioSource.Play();
    }

    public void ChangePosition(Transform t)
    {
        transform.position = t.position;
    }
}
*/
