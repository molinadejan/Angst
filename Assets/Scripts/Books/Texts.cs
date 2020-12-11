using UnityEngine;
using UnityEngine.UI;

public class Texts : MonoBehaviour
{
    // 한 페이지당 단서 하나를 출력합니다
    // 페이지를 넘길때마다 텍스트를 불러오는것이 아닌 단서를 얻으면 해당 페이지의 텍스트를 업데이트 하고
    // 페이지를 넘길때마다 이런 텍스트들을 적절히 활성화, 비활성화합니다
    // 각각의 페이지는 frontText backText 라는 양 면에 위치하게될 Text 를 가지고 있으며
    // 처음 BookUI 를 키는 시점에서 보이는 텍스트가 frontText 입니다.

    public GameObject frontPage;
    public GameObject backPage;

    public Text frontText;
    public Text backText;

    public Image frontImage;
    public Image backImage;

    [HideInInspector] public int ActivatedText; // 저장해서 불러오기 bit로 관리

    public void SetLeftTexts()
    {
        frontPage.SetActive(false);
        backPage.SetActive(true);
    }

    public void SetRightTexts()
    {
        frontPage.SetActive(true);
        backPage.SetActive(false);
    }
}
