/*
using System.Collections;
using UnityEngine;

public class LightTurnOnOff : LightBase, IEvent
{
    protected override void Awake()
    {
        base.Awake();
    }

    public void EventPlay(float t)
    {
        StartCoroutine(EventPlayCor(t));
    }

    public IEnumerator EventPlayCor(float t)
    {
        yield return new WaitForSeconds(t);

        if (pointLight.enabled)
        {
            pointLight.enabled = false;
            mat.SetColor("_EmissionColor", pointLight.color * 0);
        }
        else
        {
            pointLight.enabled = true;
            mat.SetColor("_EmissionColor", pointLight.color * pointLight.intensity);
        }
    }

    public void EventStop(float t)
    {
        StartCoroutine(EventStopCor(t));
    }

    public IEnumerator EventStopCor(float t)
    {
        yield return new WaitForSeconds(t);
    }
}
*/
