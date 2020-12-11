using UnityEngine;
using UnityEngine.Events;

// 게임에서 연출된 이벤트들을 위한 부모 클래스입니다.
// 여기에 실행될 이벤트가 저장됩니다.

public abstract class EventHandlerBase : MonoBehaviour
{
    // 인스펙터 설명용
    public string EventMemo;

    public UnityEvent events;

    public void InvokeEvent() 
    {
        events.Invoke();
        gameObject.SetActive(false);
    }
}
