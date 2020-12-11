using UnityEngine;
using UnityEngine.UI;

// 스테이지 2 에서 사용되는 클래스 입니다.
// 플레이어가 가지고 있는 인형을 관리하는 클래스 입니다.
// 게임상에서 구현해놓은 아이템의 종류가 2가지이기 때문에 
// 각각 아이템마다 하드코딩하여 수량을 관리하고 UI에 표시하는 식으로 구현하였습니다.
// 아이템이 여러개일 경우에는 적합하지 않은 구조 입니다.

public class PlayerDoll : MonoBehaviour
{
    public static PlayerDoll instance = null;

    // 인형의 개수를 출력하는 UI Text
    public Text dollCountText;

    private int dollCount;

    // 현재 가지고 있는 인형의 개수
    public int DollCount { get { return dollCount; } }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    // 인형을 얻음
    public void GetDoll()
    {
        ++dollCount;

        // 인형을 얻었다고 UI에 출력
        UIManager.instance.SetCountText(dollCountText, dollCount);
    }

    // 인형 초기화
    // 인형을 제단에 바칠때 호출됩니다.

    public int ResetDoll()
    {
        int ret = dollCount;

        dollCount = 0;

        // 인형을 몇개 바쳤다고 UI에 출력합니다.
        UIManager.instance.SetCountText(dollCountText, dollCount);

        return ret;
    }
}
