using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

// 씬 을 불러오는것을 관리하는 클래스입니다.
// 이 클래스가 부착된 오브젝트는 씬이 바뀌어도 파괴되지 않고 계속 남아있습니다.
// 특정 씬을 호출하면 자동으로 로딩씬을 거쳐가도록 구현되어있습니다.
// 비동기 씬 호출을 사용합니다.

public class LoadManager : MonoBehaviour
{
    private static LoadManager instance = null;

    public static LoadManager Instance { set { instance = value; } get { return instance; } }

    private AsyncOperation async;
    private const string LoadingScene = "Loading";

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            // 최대 프레임을 고정합니다.
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 120;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadScene(string sceneName)
    {
        // 로딩씬을 거쳐 목표 씬으로 전환을 시작합니다.
        DOTween.Clear();

        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;

        SceneManager.LoadScene(LoadingScene);
        StartCoroutine(LoadCor(sceneName));
    }

    // 목표 씬을 호출하는 코루틴
    private IEnumerator LoadCor(string sceneName)
    {
        yield return null;

        LoadingSceneManager.instance.fadeImage.DOFade(0, 5f);

        async = SceneManager.LoadSceneAsync(sceneName);
        async.allowSceneActivation = false;

        while (!async.isDone)
        {
            yield return null;
            if (async.progress >= 0.9f) break;
        }

        yield return new WaitForSeconds(9f);

        LoadingSceneManager.instance.fadeImage.DOFade(1, 2f);
        yield return new WaitForSeconds(2f);

        DOTween.Clear();

        async.allowSceneActivation = true;
    }
}
