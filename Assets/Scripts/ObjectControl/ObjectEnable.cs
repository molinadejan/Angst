using System.Collections;
using UnityEngine;

// 이벤트 인터페이스를 상속하는 오브젝트 활성화 클래스
public class ObjectEnable : MonoBehaviour, IEvent
{
    public GameObject enabledObject;

    public void EventPlay(float t)
    {
        StartCoroutine(EventPlayCor(t));
    }

    public IEnumerator EventPlayCor(float t)
    {
        yield return new WaitForSeconds(t);

        enabledObject.SetActive(true);
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
