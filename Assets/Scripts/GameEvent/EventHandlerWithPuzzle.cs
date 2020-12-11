using UnityEngine;
using UnityEngine.Events;

// 퍼즐을 성공/실패시 발동할 이벤트 클래스 입니다.

public class EventHandlerWithPuzzle : MonoBehaviour
{
    public UnityEvent successEvents;
    public UnityEvent failEvents;

    public void Success()
    {
        successEvents.Invoke();
    }

    public void Fail()
    {
        failEvents.Invoke();
    }
}
