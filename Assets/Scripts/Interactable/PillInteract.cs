
// 알약 아이템 상호작용 클래스
public class PillInteract : Interactable
{
    private void Update()
    {
        if(isEnter && InputManager.instance.interact)
        {
            PlayerStress.instance.GetPill();
            UIManager.instance.PressTextSetFalse();
            UIManager.instance.SetGetText(description + " 획득");
            UIManager.instance.PlayUISound("pillGet");
            gameObject.SetActive(false);
        }
    }
}
