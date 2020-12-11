using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

// 메인 UI를 관리하는 스크립트 입니다.

public class UIManager : MonoBehaviour 
{
    public static UIManager instance = null;

    public GameObject mainUI;
    public GameObject book;

    public Text pressEText;
    public Text getText;

    public Image fadeInOut;
    public Image moveSlow;

    public GameObject storyTeller;
    public Text textStory;

    public AudioSource[] audioSource;

    public GameObject climbText;

    public GameObject gtmBtn;
    public GameObject btnGroup;

    private bool isTurnOffUIAtThisFrame;
    private bool menuOn;
    private bool isGetPic;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            book.SetActive(true);
            book.SetActive(false);
        }
    }

    private void Update()
    {
        // Book UI를 켜고 끄는 동작을 관리합니다.
        if (!isTurnOffUIAtThisFrame && InputManager.instance.isCancle && !book.activeSelf && !menuOn)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            PlayUISound("book");

            Time.timeScale = 0f;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;

            SetMenuState(true);
            book.SetActive(true);
        }
        else if (InputManager.instance.isCancle && book.activeSelf)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            PlayUISound("bookClose");

            Time.timeScale = 1f;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;

            SetMenuState(false);
            book.SetActive(false);
        }

        isTurnOffUIAtThisFrame = false;
        climbText.SetActive(PlayerControl.instance.CanClimb && !PlayerControl.instance.IsClimb);
    }

    // 현재 활성화된 메뉴가 있는지 확인합니다.
    public bool GetMenuState()
    {
        return menuOn;
    }

    // 메뉴 활성화 함수, 이때 시간이 멈출지 흐를지 결정가능
    public void SetMenuState(bool state, bool isTimeZero = true) 
    {
        menuOn = state;
        isTurnOffUIAtThisFrame = true;

        if (state)
        {
            if(isTimeZero)
                SoundDele.pauseDele.Invoke();

            PlayerControl.instance.IsMoveLock = true;
        }
        else
        {
            if(isTimeZero)
                SoundDele.playDele.Invoke();

            PlayerControl.instance.IsMoveLock = false;
        }
    }

    public bool GetPic()
    {
        return isGetPic;
    }

    public void SetPic(bool isget)
    {
        isGetPic = isget;
    }

    // 상호작용 오브젝트에 마우스 올라갈시 아이템 정보 UI 표시
    public void PressTextSetTrue(string str)
    {
        pressEText.text = str;
        pressEText.gameObject.SetActive(true);
    }

    // 위 함수의 반대
    public void PressTextSetFalse()
    {
        pressEText.text = "";
        pressEText.gameObject.SetActive(false);
    }

    // 해당 텍스트의 카운트 수정
    public void SetCountText(Text text, int count)
    {
        text.text = "X " + count.ToString();
    }

    // 쿨타임 이미지 설정, 재생
    public void SetCountImage(Image image, float t)

    {
        image.DOKill();

        image.fillAmount = 1;
        image.gameObject.SetActive(true);

        image.DOFillAmount(0, t)
            .OnComplete(() =>
            {
                image.gameObject.SetActive(false);
            });
    }

    // 게임오버시 UI 설정
    public void GameOverBasic()
    {
        PressTextSetFalse();
    }

    // UI 사운드 출력
    public void PlayUISound(string clip)
    {
        for (int i = 0; i < audioSource.Length; i++)
        {
            if (!audioSource[i].isPlaying)
            {
                audioSource[i].clip = AudioManager.instance.GetClip(clip);
                audioSource[i].Play();
                return;
            }
        }
    }

    // 아이템 획득 텍스트 출력
    public void SetGetText(string str)
    {
        getText.DOKill();
        getText.text = str;

        getText.color = new Color(1, 1, 1, 0);
        getText.gameObject.SetActive(true);

        getText.DOColor(new Color(1, 1, 1, 1), 3f)
            .OnComplete(() =>
            {
                getText.DOColor(new Color(1, 1, 1, 0), 1f)
                .OnComplete(() =>
                {
                    getText.gameObject.SetActive(false);
                });
            });
    }

    // 메인메뉴로 나가는 버튼 함수
    public void GoToMainMenuBtnDown()
    {
        gtmBtn.SetActive(false);
        btnGroup.SetActive(true);
    }

    // 진짜 나갑니다.
    public void Yes()
    {
        LoadManager.Instance.LoadScene("MainMenu");
    }
    
    // 안 나갑니다
    public void No()
    {
        gtmBtn.SetActive(true);
        btnGroup.SetActive(false);
    }
}
