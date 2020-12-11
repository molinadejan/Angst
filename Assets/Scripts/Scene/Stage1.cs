using DG.Tweening;
using System.Collections;
using System.IO;
using UnityEngine;

// 오직 스테이지 1 에서만 사용되는 클래스 입니다.

public class Stage1 : StageCommon
{
    public GameObject mainCam;
    public LockManager lockManager;
    public string hintFilePath;

    protected override void Start()
    {
        base.Start();
        SetTextHint();
    }

    public void GameClear()
    {
        GameEnd();

        PlayerPrefs.SetInt("Stage1", 1);
        mainCam.transform.parent = null;
        StartCoroutine(GameClearCor());
    }

    private IEnumerator GameClearCor()
    {
        yield return new WaitForSeconds(11f);
        UIManager.instance.fadeInOut.color = new Color(0 ,0 ,0 ,1);
        yield return new WaitForSeconds(3f);
        LoadManager.Instance.LoadScene("Stage2");
    }

    public void GameOver()
    {
        GameEnd();

        Interactable.iHandler.Invoke();

        StressUI.instance.Damaged();
        UIManager.instance.GameOverBasic();

        StartCoroutine(GameOverCor(4f));
    }

    private IEnumerator GameOverCor(float t)
    {
        UIManager.instance.fadeInOut.DOFade(1, t);
        yield return new WaitForSeconds(t + 4f);

        LoadManager.Instance.LoadScene("Stage1");
    }

    public void SetPassword()
    {
        lockManager.SetPassword(BookUIManager.instance.pattern);
    }

    public void SetTextHint()
    {
        BookUIManager p = BookUIManager.instance;

        string str1 = "A is " + p.pageImages[0].name + ".\n";
        string str2 = "B is " + p.pageImages[1].name + ".\n";
        string str3 = "C is " + p.pageImages[2].name + ".\n";
        string str4 = "D is " + p.pageImages[3].name + ".\n";
        string str5 = "E is " + p.pageImages[4].name + ".\n";
        string str6 = "F is " + p.pageImages[5].name + ".\n";

        string str = str1 + str2 + str3 + str4 + str5 + str6;

        string filePath = Path.Combine(Application.streamingAssetsPath, hintFilePath);

        FileStream fileStream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write);
        StreamWriter streamWriter = new StreamWriter(fileStream, System.Text.Encoding.Unicode);

        streamWriter.Write(str);
        streamWriter.Close();
    }

    public void PuzzleFail(float t)
    {
        StartCoroutine(PuzzleFailCor(t));
    }

    private IEnumerator PuzzleFailCor(float t)
    {
        yield return new WaitForSeconds(t);

        PlayerStress.instance.Stress -= 30f;
    }
}
