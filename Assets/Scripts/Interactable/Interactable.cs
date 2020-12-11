using UnityEngine;

// 모든 상호작용 가능한 오브젝트들은 이 클래스를 상속합니다.
public abstract class Interactable : MonoBehaviour
{
    public delegate void InteractableDisable();
    public static InteractableDisable iHandler;

    // 상호작용 가능한 거리안에 있을 시 UI에 표시할 설명 입니다
    public string description; 
    // 상호작용 가능한 거리
    public float interactDis;
    
    // 현재 상호작용 가능한 거리 이내인가 체크
    [HideInInspector] public bool isEnter;
    // 현재 상호작용 중인가 체크
    [HideInInspector] public bool isCurActive; 

    private float interactDisSqr;

    protected virtual void Awake()
    {
        iHandler += DisableScript;
        interactDisSqr = interactDis * interactDis;
    }

    // 마우스가 오브젝트 위에 들어왔을때
    public void OnMouseEnter()
    {
        if (CheckDis() && !PlayerControl.instance.IsInteract)
        {
            isEnter = true;
            UIManager.instance.PressTextSetTrue(description);
        }
        else
        {
            isEnter = false;
        }
    }

    // 마우스가 오브젝트 위에 머물러 있을때
    public void OnMouseOver()
    {
        if(!isEnter)
        {
            if(CheckDis() && !PlayerControl.instance.IsInteract)
            {
                isEnter = true;
                UIManager.instance.PressTextSetTrue(description);
            }
        }
    }

    // 마우스가 오브젝트에서 나갈때
    public void OnMouseExit()
    {
        isEnter = false;
        UIManager.instance.PressTextSetFalse();
    }

    // 거리 재기
    public bool CheckDis()
    {
        return Vector3.SqrMagnitude(transform.position - PlayerControl.instance.transform.position) <= interactDisSqr;
    }

    // 게임 내 시간 제어
    public void SetTimescale(float t)
    {
        Time.timeScale = t;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
    }

    // 마우스의 고정과 커서 활성화 제어
    public void SetMouseVisible(bool check)
    {
        Cursor.lockState = check ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = check;
    }

    private void DisableScript()
    {
        gameObject.SetActive(false);
    }

    public void OnDestroy()
    {
        iHandler -= DisableScript;
    }
}
