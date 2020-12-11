using System.Collections;
using UnityEngine;

// 이벤트 인터페이스를 상속하는 오브젝트 비활성화 클래스
public class ObjectDisable : MonoBehaviour, IEvent
{
    public void EventPlay(float t)
    {
        StartCoroutine(EventPlayCor(t));
    }

    public IEnumerator EventPlayCor(float t)
    {
        yield return new WaitForSeconds(t);

        gameObject.SetActive(false);
    }

    public void EventStop(float t)
    {
        throw new System.NotImplementedException();
    }

    public IEnumerator EventStopCor(float t)
    {
        throw new System.NotImplementedException();
    }
}
