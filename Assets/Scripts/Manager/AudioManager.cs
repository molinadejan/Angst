using System.Collections.Generic;
using UnityEngine;


// 게임에서 사용되는 모든 사운드 파일들을 관리하는 클래스입니다.
// 딕셔너리에 파일 이름, 사운드 파일 쌍으로 데이터를 저장하고
// 반대로 가져올수도 있게 구현하였습니다.

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance = null;
    private Dictionary<string, AudioClip> clipData;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            clipData = new Dictionary<string, AudioClip>();

            // 해당 폴더 내에 있는 모든 사운드 파일들을 가져옵니다.
            AudioClip[] clips = Resources.LoadAll<AudioClip>("Sounds");

            for(int i = 0; i < clips.Length; i++)
            {
                clipData.Add(clips[i].name, clips[i]);
            }
        }
    }

    // 딕셔너리에서 이름을 키 값으로 사운드 파일을 가져옵니다.
    public AudioClip GetClip(string clipName)
    {
        if (clipData.ContainsKey(clipName))
            return clipData[clipName];

        return null;
    }
}
