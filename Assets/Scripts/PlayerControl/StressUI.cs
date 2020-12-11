using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

// 스트레스 값만 읽어서 UI 표시하는 클래스 입니다.

public class StressUI : MonoBehaviour
{
    public static StressUI instance = null;

    public Slider stressSlider;
    public Image damagedFade;
    private bool isDamage;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Update()
    {
        stressSlider.value = PlayerStress.instance.Stress;
    }

    // 데미지를 받았을때 화면을 붉게 합니다.
    public void Damaged()
    {
        damagedFade.color = new Color32(255, 0, 0, 50);

        if (isDamage)
            damagedFade.DOKill();

        damagedFade.DOFade(0, 5f)
            .OnStart(() => isDamage = true)
            .OnComplete(() => isDamage = false);
    }
}
