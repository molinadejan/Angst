/*
using System.Collections;
using UnityEngine;

public class GirlSilhouetteScreen : MonoBehaviour, IEvent
{
    public SpriteRenderer silhouette;
    private bool isBlink = false;

    public void EventPlay(float t)
    {
        StartCoroutine(EventPlayCor(t));
    }

    public IEnumerator EventPlayCor(float t)
    {
        yield return new WaitForSeconds(t);

        ScreenEffects screenEffects = ScreenEffects.instance;

        isBlink = true;

        screenEffects.sbc.enabled = true;
        screenEffects.secondCam.SetActive(true);

        while(isBlink)
        {
            int ran = Random.Range(0, 2);

            screenEffects.sbc.intensity = Random.Range(0f, 30f);
            screenEffects.sbc.shift = Random.Range(-5f, 5f);

            silhouette.enabled = ran == 0 ? false : true;
            screenEffects.sbc.enabled = ran == 0 ? false : true;

            yield return new WaitForSeconds(Random.Range(0.05f, 0.1f));
        }

        screenEffects.sbc.enabled = false;
        screenEffects.secondCam.SetActive(false);
    }

    public void EventStop(float t)
    {
        StartCoroutine(EventStopCor(t));
    }

    public IEnumerator EventStopCor(float t)
    {
        yield return new WaitForSeconds(t);
        isBlink = false;
    }
}
*/