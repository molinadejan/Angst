/*
using System.Collections;
using UnityEngine;

public class GirlSilhouette : MonoBehaviour, IEvent
{
    public LightBlink lb;

    private SpriteRenderer sr;
    private bool isBlink = false;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        sr.color = new Color(1, 1, 1, 0);
    }

    public void EventPlay(float t)
    {
        StartCoroutine(EventPlayCor(t));
    }

    public IEnumerator EventPlayCor(float t)
    {
        yield return new WaitForSeconds(t);

        isBlink = true;

        while(isBlink)
        {
            sr.color = lb.intensity == 0 ? new Color32(255, 255, 255, 0) : new Color32(255, 255, 255, 20);

            yield return null;
        }
    }

    public void EventStop(float t)
    {
        StartCoroutine(EventStopCor(t));
    }

    public IEnumerator EventStopCor(float t)
    {
        yield return new WaitForSeconds(t);

        isBlink = false;
        sr.color = new Color(1, 1, 1, 0);
    }
}
*/
