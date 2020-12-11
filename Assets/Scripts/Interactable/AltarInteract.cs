
using UnityEngine;

// 스테이지2 제단 상호작용 클래스

public class AltarInteract : Interactable
{
    public int goalCount;

    public GameObject[] clues;
    private int totalDollCount;

    private void Update()
    {
        if(isEnter && InputManager.instance.interact)
        {
            if(PlayerDoll.instance.DollCount == 0)
            {
                UIManager.instance.SetGetText("가지고 있는 인형이 없습니다");
            }
            else
            {
                int getCount = PlayerDoll.instance.ResetDoll();

                int prevDollCount = totalDollCount;
                totalDollCount += getCount;

                for(int i = prevDollCount; i < totalDollCount; i++)
                {
                    clues[i].gameObject.SetActive(true);
                }

                UIManager.instance.SetGetText("인형을 " + getCount + "개 바침");
            }
        }
    }
}
