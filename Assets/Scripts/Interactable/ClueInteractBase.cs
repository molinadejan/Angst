#pragma warning disable CS0649

using UnityEngine;

// 모든 단서들은 CludeBase 클래스를 상속 받습니다.
// ClueBase 클래스에 현재 얻은 단서인지, 현재 UI로 보고 있는 단서인지, 현재 마우스가 올라가있는지
// 확인하고 저장하는 변수들을 관리합니다.

// 또한 조건을 확인하고 실행할 이벤트 변수와 이벤트의 조건을 확인할 함수도 정의되어 있습니다
// eh 변수는 특정 이벤트의 조건이 되는 단서가 아니라면 그냥 대입없이 두어도 괜찮습니다.

public abstract class ClueInteractBase : Interactable
{
    // 얻은 단서인지 체크
    [HideInInspector] public bool isGet = false;

    // 텍스트 단서일 경우 몇번째 페이지에 추가될지를 결정합니다.
    public int clueNum;

    // 이벤트 핸들러
    [HideInInspector] public EventHandlerWithCondition eh = null;

    protected override void Awake()
    {
        base.Awake();
    }

    public void CheckCondition()
    {
        if (eh != null && eh.gameObject.activeSelf)
        {
            eh.CheckCondition();
        }
    }
}
