using System.Collections;

// 이 인터페이스를 상속하면 이벤트 클래스의 이벤트 변수에 추가가 가능해집니다.
public interface IEvent
{
    // 이벤트를 t초뒤 활성화 합니다
    void EventPlay(float t);

    // 이벤트를 t초뒤 비활성화 합니다
    void EventStop(float t);

    // 이벤트 활성화 코루틴
    IEnumerator EventPlayCor(float t);

    // 이벤트 비활성화 코루틴
    IEnumerator EventStopCor(float t);
}
