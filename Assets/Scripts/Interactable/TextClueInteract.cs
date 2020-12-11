using System.IO;
using UnityEngine;
using System.Text;

// 단서와의 상호작용을 정의한 클래스
public class TextClueInteract : ClueInteractBase
{
    private void Update()
    {
        if (!isCurActive && isEnter && InputManager.instance.interact)
        {
            UIManager.instance.PressTextSetFalse();
            UIManager.instance.PlayUISound("book");

            PlayerControl.instance.IsInteract = true;
            UIManager.instance.SetMenuState(true);
            
            isCurActive = true;

            SetTimescale(0f);

            UIManager.instance.storyTeller.SetActive(true);
            UIManager.instance.textStory.text = CallTextFile();
        }

        if (isCurActive && InputManager.instance.isCancle)
        {
            PlayerControl.instance.IsInteract = false;
            UIManager.instance.SetMenuState(false);
            
            isCurActive = false;

            SetTimescale(1f);

            UIManager.instance.storyTeller.SetActive(false);
            
            isGet = true;
            BookUIManager.instance.AddProvision(clueNum, UIManager.instance.textStory.text);
            UIManager.instance.SetGetText(description + " 획득");
            CheckCondition();
            gameObject.SetActive(false);

            UIManager.instance.textStory.text = "";
        }
    }

    // 텍스트를 불러와 UI에 출력합니다.

    private string CallTextFile()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, "Text/" + gameObject.name + ".txt");

        FileInfo fileInfo = new FileInfo(filePath);
        string ret = "";

        if (fileInfo.Exists)
        {
            StreamReader reader = new StreamReader(filePath, Encoding.Default);
            ret = reader.ReadToEnd();
            reader.Close();
        }

        return ret;
    }
}
