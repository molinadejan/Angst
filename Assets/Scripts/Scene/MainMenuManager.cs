using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

// 게임 시작 메인 메뉴 화면을 관리하는 클래스 입니다.
// 각종 버튼의 동작을 정의합니다.

public class MainMenuManager : MonoBehaviour
{
    public GameObject btnGroup;
    public GameObject htpUI;
    public GameObject warningUI;
    public GameObject continueUI;

    public GameObject stage1Btn;
    public GameObject stage2Btn;

    public GameObject stage1ClearText;
    public GameObject stage2ClearText;

    public PlayableDirector pd;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if(PlayerPrefs.GetInt("Stage1") == 0)
        {
            stage2Btn.SetActive(false);
            stage1ClearText.SetActive(false);
        }
        else
        {
            stage2Btn.SetActive(true);
            stage1ClearText.SetActive(true);
        }

        if (PlayerPrefs.GetInt("Stage2") == 0)
        {
            stage2ClearText.SetActive(false);
        }
        else
        {
            stage2ClearText.SetActive(true);
        }
    }

    public void NewBtnDown()
    {
        btnGroup.SetActive(false);
        warningUI.SetActive(true);

        pd.Pause();
    }

    public void YesBtnDown()
    {
        warningUI.SetActive(false);

        SaveManager.instance.ResetData();

        StartCoroutine(YesBtnDownCor());
    }

    private IEnumerator YesBtnDownCor()
    {
        yield return new WaitForSeconds(1f);
        LoadManager.Instance.LoadScene("Stage1");
    }

    public void NoBtnDown()
    {
        btnGroup.SetActive(true);
        warningUI.SetActive(false);

        pd.Resume();
    }

    public void ContinueBtnDown()
    {
        btnGroup.SetActive(false);
        continueUI.SetActive(true);

        pd.Pause();
    }

    public void ContinueExitBtnDown()
    {
        btnGroup.SetActive(true);
        continueUI.SetActive(false);

        pd.Resume();
    }


    public void HowToPlayBtnDown()
    {
        btnGroup.SetActive(false);
        htpUI.SetActive(true);

        pd.Pause();
    }

    public void OKBtnDown()
    {
        btnGroup.SetActive(true);
        htpUI.SetActive(false);

        pd.Resume();
    }

    public void ExitBtnDown()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    public void LoadScene(string name)
    {
        LoadManager.Instance.LoadScene(name);
    }
}
