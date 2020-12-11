using UnityEngine;
using DG.Tweening;

// 소리의 근원과 플레이어 사이의 오브젝트들을 체크하여 
// 볼륨을 조절하거나 고주파 영역을 차단시키는 역할을 하는 클래스 입니다.

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(AudioLowPassFilter))]
public class SoundWithWall : MonoBehaviour
{
    private const float DEFAULT_PASS = 5000f;

    public LayerMask terrainMask;

    // 차단할 주파수 영역
    public float cutOffPass;

    // 소리의 감쇠가 일어나는 벽 수
    public float wallCount;

    private AudioSource source;
    private AudioLowPassFilter lowPass;
    private Transform targetTrans;
    private Vector3 target;

    public float Volume
    {
        get { return source.volume; }
        set
        {
            source.DOKill();
            source.DOFade(value, 1f);
        }
    }

    private float volume;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
        lowPass = GetComponent<AudioLowPassFilter>();
        targetTrans = FindObjectOfType<PlayerControl>().center;
        volume = source.volume;
    }

    private void Update()
    {
        if (source.isPlaying)
        {
            int cnt = CheckTerrainCount(Vector3.Distance(targetTrans.position, transform.position));

            if(cnt > 0)
            {
                if(cnt >= wallCount)
                {
                    Volume = volume / 3;
                }

                lowPass.cutoffFrequency = cutOffPass;
            }
            else
            {
                Volume = volume;
                lowPass.cutoffFrequency = DEFAULT_PASS;
            }
        }
    }

    // 플레이어와 사운드 근원 사이의 오브젝트의 개수를 반환
    private int CheckTerrainCount(float distance)
    {
        target = targetTrans.position - transform.position;
        RaycastHit[] hits = Physics.RaycastAll(transform.position, target, distance, terrainMask);
        return hits.Length;
    }
}
