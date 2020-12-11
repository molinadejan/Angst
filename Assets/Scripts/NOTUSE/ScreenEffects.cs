/*
#pragma warning disable CS0649

using System.Collections;
using UnityEngine;

public class ScreenEffects : MonoBehaviour
{
    public static ScreenEffects instance = null;

    public ShaderEffect_BleedingColors sbc;
    public ShaderEffect_Unsync su;
    public ShaderEffect_Tint st;
    public ShaderEffect_CRT sc;
    public ShaderEffect_CorruptedVram scv;

    public GameObject secondCam;

    private IEnumerator sbcCor;

    [HideInInspector] public float sbcIntensity;
    [HideInInspector] public float sbcShift;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void StartSBCCor()
    {
        sbcCor = SBCCor();
        StartCoroutine(sbcCor);
        sbc.enabled = true;
    }

    private IEnumerator SBCCor()
    {
        while(true)
        {
            float intensity = Random.Range(sbcIntensity / 2, sbcIntensity);
            float shift     = Random.Range(-sbcShift, sbcShift);
            float wait      = Random.Range(0.05f, 0.1f);

            sbc.intensity = intensity;
            sbc.shift     = shift;

            yield return new WaitForSeconds(wait);
        }
    }

    public void StopSBCCor()
    {
        if (sbcCor != null)
        {
            StopCoroutine(sbcCor);
            sbcCor = null;
        }

        sbc.enabled = false;
    }
}
*/