using UnityEngine;

// 플레이어의 스테이지 진행상황을 저장, 관리하는 클래스입니다.

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            if (!PlayerPrefs.HasKey("Stage1"))
            {
                PlayerPrefs.SetInt("Stage1", 0);
            }

            if (!PlayerPrefs.HasKey("Stage2"))
            {
                PlayerPrefs.SetInt("Stage2", 0);
            }
        }
    }

    // 데이터 초기화
    public void ResetData()
    {
        PlayerPrefs.SetInt("Stage1", 0);
        PlayerPrefs.SetInt("Stage2", 0);
    }
}
