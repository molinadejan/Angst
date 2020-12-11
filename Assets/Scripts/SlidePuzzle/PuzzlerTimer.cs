using UnityEngine;
using UnityEngine.UI;

// 퍼즐의 타이머 정의 클래스

public class PuzzlerTimer : MonoBehaviour
{
    public Slider sTimer;
    bool isEnd = false;

    void Update()
    {
        if (sTimer.value > 0f)
        {
            sTimer.value -= Time.deltaTime * 3;
        }
        else
        {
            isEnd = true;
        }
    }

    public bool GetEnd()
    {
        return isEnd;
    }

    public void SetTime(float timer)
    {
        // isEnd도 false로 해줘야 타임오버되고 다시 시도할 경우 바로 꺼지는 현상이 나타나지 않습니다.
        isEnd = false;
        sTimer.value = timer;
    }
}
