using UnityEngine;
using System.IO;

// 각 스테이지의 클리어 패스워드를 생성하고 텍스트 파일에 작성하는 클래스 입니다.

public class PasswordManager : MonoBehaviour
{
    public LockManager lockManager;
    public string answer;
    public string answerFilePath;

    private void Start()
    {
        SetPassword();
    }

    // 패스워드를 생성하여 단서로 출력이 가능하게 txt 파일로 저장합니다.
    public void SetPassword()
    {
        answer = lockManager.MakePassword();

        string filePath = Path.Combine(Application.streamingAssetsPath, answerFilePath);

        FileStream fileStream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write);
        StreamWriter streamWriter = new StreamWriter(fileStream, System.Text.Encoding.Unicode);

        streamWriter.Write(answer);
        streamWriter.Close();
    }
}

