
using UnityEngine;

// 보통 라이트 클래스
public class LightNormal : MonoBehaviour
{
    private Material mat;

    public Light pointLight = null;
    public Light spotLight = null;

    private void Awake()
    {
        mat = GetComponent<Renderer>().material;

        if (pointLight == null)
        {
            mat.SetColor("_EmissionColor", Color.white * 0);
        }
        else
        {
            mat.SetColor("_EmissionColor", pointLight.color * pointLight.intensity);
        }
    }
}
