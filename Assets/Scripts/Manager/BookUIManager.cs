using System.Collections.Generic;
using UnityEngine;

// 단서를 획득할때마다 Book UI의 페이지에 랜덤한 문양을 추가하고 
// 텍스트를 설정하는 스크립트 입니다.

public class BookUIManager : MonoBehaviour
{
    public static BookUIManager instance = null;

    // 페이지 문양에 쓰일 이미지 리스트 입니다.
    public List<Sprite> pageImages;

    [HideInInspector] public string pattern = "";

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            RandomGenerate();
        }
    }

    // 스테이지 1에서 페이지 문양이 자물쇠의 암호를 푸는 힌트로 사용됩니다.
    // 따라서 문양이 알파벳과 랜덤하게 매칭되도록 섞어줍니다.

    private void RandomGenerate()
    {
        List<Sprite> pageImagesMix = new List<Sprite>();

        int len = pageImages.Count;

        for(int i = 0; i < len; i++)
        {
            int index = Random.Range(0, pageImages.Count);
            pageImagesMix.Add(pageImages[index]);
            pageImages.RemoveAt(index);
        }

        pageImages = pageImagesMix;
    }

    // Book UI 의 페이지에 텍스트와 문양을 추가합니다.
    public void AddProvision(int provisionNum, string text = "")
    {
        int index = provisionNum / 2;
        
        if (index >= Book.instance.pages.Count) return;

        int imageNum = Random.Range(0, pageImages.Count);

        pattern += imageNum.ToString();

        if(provisionNum % 2 == 0)
        {
            Book.instance.pages[index].texts.frontText.text = text;
            Book.instance.pages[index].texts.frontImage.sprite = pageImages[imageNum];
        }
        else
        {
            Book.instance.pages[index].texts.backText.text = text;
            Book.instance.pages[index].texts.backImage.sprite = pageImages[imageNum];
        }
    }
}
