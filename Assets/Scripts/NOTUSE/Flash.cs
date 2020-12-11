/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flash : MonoBehaviour
{
    public float totalSeconds;     
    public float maxIntensity;     
    public Light myLight;        
    public float wTime;
    public float sTime;
    public int cnt = 0;
    const int MaxCnt = 4;
    public IEnumerator flashNow()
    {
        while (true)
        {
            float waitTime = totalSeconds / 2;

            while (myLight.intensity < maxIntensity)
            {
                myLight.intensity += Time.deltaTime / waitTime;        
                yield return null;
            }
            while (myLight.intensity > 0)
            {
                myLight.intensity -= Time.deltaTime / waitTime;       
                yield return null;
            }
            while ((wTime > sTime) && cnt==MaxCnt)
            {
                sTime += Time.deltaTime;
                yield return null;
            }
            if (cnt == MaxCnt)
            {
                cnt = 0;
                yield return null;
            }
            sTime = 0;
            cnt++;
            yield return null;
        }
    }

    private void Start()
    {
        StartCoroutine(flashNow());
    }
}
*/
