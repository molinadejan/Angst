using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

// UI버튼에 마우스가 올라갈시 배경 이미지를 채우는 클래스 입니다.

public class ButtonImage : MonoBehaviour , IPointerEnterHandler,IPointerExitHandler 
{
    public Image image;

    public void OnPointerEnter(PointerEventData eventData)
    {
        image.DOKill();
        image.DOFillAmount(1, 0.5f).SetUpdate(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        image.DOKill();
        image.DOFillAmount(0, 0.5f).SetUpdate(true);
    }

    public void OnDisable()
    {
        image.DOKill();
        image.fillAmount = 0;
    }
}
