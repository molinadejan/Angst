/*
using System.Collections;
using UnityEngine;

public class LightColorChange : LightBase, IEvent
{
    public Color changeColor;
    public float changeIntensity;

    private Color originColor;
    private float originIntensity;

    protected override void Awake()
    {
        base.Awake();
        originColor = pointLight.color;
        originIntensity = pointLight.intensity;
    }

    public void EventPlay(float t)
    {
        StartCoroutine(EventPlayCor(t));
    }

    public IEnumerator EventPlayCor(float t)
    {
        yield return new WaitForSeconds(t);

        if(pointLight.color == originColor)
        {
            pointLight.color = changeColor;
            pointLight.intensity = changeIntensity;
        }
        else
        {
            pointLight.color = originColor;
            pointLight.intensity = originIntensity;
        }

        mat.SetColor("_EmissionColor", pointLight.color * pointLight.intensity);
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
