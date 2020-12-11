using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

// 특정 연출 씬을 위한 타임라인 컨트롤을 위한 클래스
public class TimeLineControl : MonoBehaviour, IEvent
{
    private PlayableDirector pd;

    private void Awake()
    {
        pd = GetComponent<PlayableDirector>();
    }

    public void EventPlay(float t)
    {
        StartCoroutine(EventPlayCor(t));
    }

    public IEnumerator EventPlayCor(float t)
    {
        yield return new WaitForSeconds(t);
        pd.Play();
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
