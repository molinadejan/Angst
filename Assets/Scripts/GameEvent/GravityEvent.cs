using System.Collections;
using UnityEngine;

// 중력 이벤트 실행 클래스 입니다.

public class GravityEvent : MonoBehaviour, IEvent
{
    public Vector3 gravityDir;
    public float lookUpAngle;

    public void EventPlay(float t)
    {
        StartCoroutine(EventPlayCor(t));
    }

    public IEnumerator EventPlayCor(float t)
    {
        yield return new WaitForSeconds(t);
        GravityChangeManager.instance.ChangeGravity(gravityDir, transform.position, lookUpAngle);
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
