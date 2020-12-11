using UnityEngine;

// 특정 콜라이더에 충돌시 발생하는 이벤트 클래스 입니다.
public class EventHandlerWithCollider : EventHandlerBase
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            InvokeEvent();
        }
    }
}
