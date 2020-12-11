using System.Collections;
using UnityEngine;

// 깜박이는 라이트 정의 클래스
public class LightBlink : MonoBehaviour, IEvent
{
    private Material mat;

    public Light pointLight;
    public Light spotLight;

    private float minMain = 0.05f;
    private float maxMain = 1.2f;

    private float minDelay = 0.02f;
    private float maxDelay = 0.1f;

    private void Awake()
    {
        mat = GetComponent<Renderer>().material;
        mat.SetColor("_EmissionColor", pointLight.color * pointLight.intensity);
    }

    private void Start()
    {
        StartCoroutine(BlinkCor());
    }

    private IEnumerator BlinkCor()
    {
        while (true)
        {
            float waitTime = 0;
            float emission = 0;
            float intensity = 0;

            waitTime = Random.Range(minDelay, maxDelay);
            intensity = Random.Range(minMain, maxMain);
            emission = intensity / maxMain;

            pointLight.intensity = intensity;
            spotLight.intensity = intensity * 10f;

            mat.SetColor("_EmissionColor", pointLight.color * emission);

            yield return new WaitForSeconds(waitTime);
        }
    }

    public void EventPlay(float t)
    {
        throw new System.NotImplementedException();
    }

    public void EventStop(float t)
    {
        throw new System.NotImplementedException();
    }

    public IEnumerator EventPlayCor(float t)
    {
        throw new System.NotImplementedException();
    }

    public IEnumerator EventStopCor(float t)
    {
        throw new System.NotImplementedException();
    }
}
