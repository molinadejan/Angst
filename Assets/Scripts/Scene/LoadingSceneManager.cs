using UnityEngine;
using UnityEngine.UI;

// 로딩화면을 관리하는 클래스입니다.

public class LoadingSceneManager : MonoBehaviour
{
    public static LoadingSceneManager instance = null;

    // 로딩 이미지 파일 목록
    public Sprite[] sprites;

    // 로딩 이미지를 출력할 이미지 오브젝트
    public Image bg;
    // 페이드 인 아웃에 쓰일 이미지
    public Image fadeImage;

    private void Awake()
    {
        // 랜덤으로 한장 골라서 풀력합니다.
        if(instance == null)
        {
            instance = this;
            bg.sprite = sprites[Random.Range(0, sprites.Length)];
        }
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
