#pragma warning disable CS0649

using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// 플레이어의 스트레스 저항수치, 가지고 있는 알약 아이템을 관리하는 클래스입니다. UI출력또한 관리합니다.
// 플레이어의 스트레스 저항수치가 0이 되면 게임오버입니다. 시간이 지나면 천천히 회복하며 알약 아이템으로도 회복이 가능합니다.
// 게임상에서 구현해놓은 아이템의 종류가 2가지이기 때문에 
// 각각 아이템마다 하드코딩하여 수량을 관리하고 UI에 표시하는 식으로 구현하였습니다.
// 아이템이 여러개일 경우에는 적합하지 않은 구조 입니다.

public class PlayerStress : MonoBehaviour
{
    public static PlayerStress instance = null;

    // 가지고 있는 알약 개수 텍스트
    public Text pillCountText;
    // 알약 쿨타임을 시계방향 이미지 커버로 표현합니다.
    public Image pillcoolTimeImage;

    // 플레이어가 데미지를 받을때 출력할 오디오 클립이름 입니다.
    public string painSoundClipName;
    // 플레이어가 알약을 사용할때 출력할 오디오 클립 이름 입니다.
    public string pillUseSoundclipName;

    // 게임 오버시 실행할 이벤트 입니다.
    public UnityEvent gameOverEvent;

    private float stress = 100;

    // 알약을 먹었을 경우 회복 수치
    [SerializeField] private float pillRecover;

    // 알약 사용 쿨타임
    [SerializeField] private float pillDelay;

    // 초마다 자연 치유 되는 스트레스 저항수치
    [SerializeField] private float recoverFloat;

    private bool isPillDelay;
    private float pillDelayCount;
    private int pillCount;

    public float Stress
    { 
        set 
        {
            // 데미지를 받음
            if (stress > value && value > 0)
            {
                PlayerControl.instance.SoundPlay(painSoundClipName, false);
                StressUI.instance.Damaged();
            }

            stress = value;

            // 스트레스 저항수치 회복
            if (stress > 100f)
                stress = 100f;

            // 게임오버
            if (stress <= 0f)
            {
                stress = 0;
                gameOverEvent.Invoke();
            }
        } 
        get 
        { 
            return stress;
        } 
    }

    public void Damaged(float damage)
    {
        Stress -= damage;
    }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    private void Update()
    {
        // 시간이 지날수록 자연치유
        Stress += Time.deltaTime * recoverFloat;

        // 알약사용
        if(InputManager.instance.pillUse && !UIManager.instance.GetMenuState() && !PlayerControl.instance.IsMoveLock)
        {
            UsePill();
        }
    }

    // 알약 획득
    public void GetPill()
    {
        ++pillCount;
        UIManager.instance.SetCountText(pillCountText, pillCount);
    }

    // 알약 사용
    public void UsePill()
    {
        if (pillCount <= 0 || stress >= 100 || isPillDelay)
        {
            return;
        }

        --pillCount;
        // 알약 개수 텍스트와 쿨타임 이미지 수정
        UIManager.instance.SetCountText(pillCountText, pillCount);
        UIManager.instance.SetCountImage(pillcoolTimeImage, pillDelay);

        // 알약 먹는 사운드 출력
        UIManager.instance.PlayUISound(pillUseSoundclipName);

        Stress += pillRecover;

        // 딜레이 계산
        DOTween.To(() => pillDelayCount, x => pillDelayCount = x, pillDelay, pillDelay)
            .SetEase(Ease.Linear)
            .OnStart(() =>
            {
                isPillDelay = true;
            })
            .OnComplete(() =>
            {
                isPillDelay = false;
                pillDelayCount = 0;
            });
    }
}
