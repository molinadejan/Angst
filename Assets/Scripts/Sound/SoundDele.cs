using UnityEngine;

// 메인 UI를 키거나 다른 오브젝트와 상호작용할때 시간이 멈추게 되는 경우가 있는데
// 시간이 멈출 경우 주변 사운드를 일시 정지하기위해 델리게이트를 선언하여
// 간편하게 모든 오브젝트 사운드를 컨트롤 가능하도록 설계된 클래스 입니다.

[RequireComponent (typeof(AudioSource))]
public class SoundDele : MonoBehaviour
{
    public delegate void PauseDele();
    public delegate void PlayDele();

    public static PauseDele pauseDele;
    public static PlayDele playDele;

    private AudioSource audioSource;

    private bool isPause;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        pauseDele += Pause;
        playDele += Play;
    }

    public void Pause()
    {
        if (audioSource.isPlaying)
        {
            isPause = true;
            audioSource.Pause();
        }
    }

    public void Play()
    {
        if (isPause)
        {
            isPause = false;
            audioSource.Play();
        }
    }

    public void OnDestroy()
    {
        pauseDele -= Pause;
        playDele -= Play;
    }
}
