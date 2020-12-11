using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

// 스테이지2에서만 사용되는 클래스입니다.
public class Stage2 : StageCommon
{
    public GameObject mapObject;
    public NavMeshSurface navMeshSurface;

    public GameObject mainCam;
    public GameObject secondCam;

    // 게임 시작 전에 미리 적 오브젝트가 이동 가능한 영역을 계산하기 위해 회전을 한 후
    // 계산을 하고 다시 원상태로 돌려놓는 작업입니다.

    private void Awake()
    {
        mapObject.transform.position = new Vector3(103, 10, 0);
        mapObject.transform.eulerAngles = new Vector3(0, 0, 180);
    }

    protected override void Start()
    {
        base.Start();

        navMeshSurface.BuildNavMesh();
        mapObject.transform.eulerAngles = Vector3.zero;
        mapObject.transform.position = Vector3.zero;
    }

    public void GameClear()
    {
        GameEnd();
        PlayerPrefs.SetInt("Stage2", 1);
        StartCoroutine(GameClearCor());
    }

    private IEnumerator GameClearCor()
    {
        UIManager.instance.fadeInOut.DOFade(1, 3f);
        yield return new WaitForSeconds(3f);
        LoadManager.Instance.LoadScene("MainMenu");
    }

    public void GameOver()
    {
        GameEnd();

        Interactable.iHandler.Invoke();

        Sequence s = DOTween.Sequence()
            .Append(UIManager.instance.fadeInOut.DOColor(new Color(0, 0, 0, 1), 2f)
                .OnStart(() =>
                {
                    UIManager.instance.GameOverBasic();
                    UIManager.instance.fadeInOut.gameObject.SetActive(true);
                })
                .OnComplete(() =>
                {
                    UIManager.instance.fadeInOut.gameObject.SetActive(false);
                    secondCam.SetActive(true);
                }))
            .Append(secondCam.transform.DORotate(new Vector3(transform.rotation.x, 0, 0), 4f)
                .OnComplete(() =>
                {
                    MotherEnemyScreen.instance.SetAttack();
                }))
            .Insert(7.7f, UIManager.instance.fadeInOut.DOColor(new Color(0, 0, 0, 1), 0.1f)
                .OnStart(() => 
                {
                    UIManager.instance.fadeInOut.gameObject.SetActive(true);
                })
                .OnComplete(() =>
                {
                    StartCoroutine(GameOverCor());
                }));
    }

    private IEnumerator GameOverCor()
    {
        yield return new WaitForSeconds(3f);
        LoadManager.Instance.LoadScene("Stage2");
    }
}
