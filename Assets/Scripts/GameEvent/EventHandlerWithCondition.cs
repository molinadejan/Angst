using System.Collections.Generic;

// 특정 단서를 얻었을시 발동되는 이벤트 클래스 입니다.
public class EventHandlerWithCondition : EventHandlerBase
{
    public List<ClueInteractBase> clue;

    private void Awake()
    {
        foreach (ClueInteractBase c in clue)
        {
            c.eh = this;
        }
    }

    public void CheckCondition()
    {
        foreach (ClueInteractBase c in clue)
        {
            if (!c.isGet) return;
        }

        InvokeEvent();
    }
}
