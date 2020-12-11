using UnityEngine;

// 이미지가 포함된 단서와의 상호작용을 정의한 클래스

public class ImageClueInteract : ClueInteractBase
{
    public GameObject canvas;

    void Update()
    {
        if (!isCurActive && isEnter && InputManager.instance.interact)
        {
            UIManager.instance.PressTextSetFalse();
            UIManager.instance.PlayUISound("book");

            PlayerControl.instance.IsInteract = true;
            UIManager.instance.SetMenuState(true);

            isCurActive = true;

            SetTimescale(0f);

            canvas.SetActive(true);

            UIManager.instance.SetPic(true);
        }

        if (isCurActive && InputManager.instance.isCancle)
        {
            PlayerControl.instance.IsInteract = false;
            UIManager.instance.SetMenuState(false);

            isCurActive = false;

            SetTimescale(1f);

            canvas.SetActive(false);
            
            isGet = true;
            UIManager.instance.SetGetText(description);
            CheckCondition();
            gameObject.SetActive(false);
        }
    }
}
