using UnityEngine;

// 퍼즐과의 상호작용을 정의한 클래스입니다.

public class PuzzleInteract : Interactable
{
    public GameObject puzzleUI;
    public PuzzlerTimer sTimer;
    public GameObject pic;

    public EventHandlerWithPuzzle puzzleEvents;

    private void Update()
    {
        if(!isCurActive && isEnter && InputManager.instance.interact)
        {
            UIManager.instance.PressTextSetFalse();

            PlayerControl.instance.IsInteract = true;

            UIManager.instance.SetMenuState(true, false);

            isCurActive = true;

            SetMouseVisible(true);

            puzzleUI.SetActive(true);

            pic.SetActive(true);
        }

        if(isCurActive && (InputManager.instance.isCancle || sTimer.GetEnd() || SlidePuzzleManager.instance.CheckWin()))
        {
            PlayerControl.instance.IsInteract = false;

            UIManager.instance.SetMenuState(false, false);

            isCurActive = false;

            SetMouseVisible(false);

            puzzleUI.SetActive(false);

            sTimer.SetTime(100f);

            // 퍼즐을 풀었는지 확인, 성공이면 성공 이벤트 발동
            if(SlidePuzzleManager.instance.CheckWin())
            {
                puzzleEvents.Success();
                gameObject.SetActive(false);
                UIManager.instance.PressTextSetFalse();
            }
            // 실패이벤트 발동
            else
            {
                puzzleEvents.Fail();
            }
        }
    }
}
