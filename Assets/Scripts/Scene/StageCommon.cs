using DG.Tweening;
using UnityEngine;

// 스테이지 공통 루틴 정의 클래스

public abstract class StageCommon : MonoBehaviour
{
    protected virtual void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        GameStart();
    }

    protected void GameEnd()
    {
        UIManager.instance.mainUI.SetActive(false);
        InputManager.instance.AllInputBlock();
        PlayerControl.instance.IsMoveLock = true;
    }

    private void GameStart()
    {
        UIManager.instance.fadeInOut.DOFade(0, 6f)
            .OnStart(() => PlayerControl.instance.IsMoveLock = true)
            .OnComplete(() => PlayerControl.instance.IsMoveLock = false);
    }
}
