using System.Collections;
using UnityEngine;

public class LockInteract : Interactable
{
    public GameObject lockCam;
    [HideInInspector] public bool isUnlock;

    public EventHandlerWithPuzzle eventHandler;

    private void Update()
    {
        if(!isCurActive && isEnter && InputManager.instance.interact)
        {
            UIManager.instance.PressTextSetFalse();
            //UIManager.instance.PlayUISound("book");

            PlayerControl.instance.IsInteract = true;
            UIManager.instance.SetMenuState(true, false);

            isCurActive = true;

            SetMouseVisible(true);

            lockCam.SetActive(true);
        }

        if(isCurActive && InputManager.instance.isCancle && !isUnlock)
        {
            PlayerControl.instance.IsInteract = false;
            UIManager.instance.SetMenuState(false, false);

            isCurActive = false;

            SetMouseVisible(false);

            lockCam.SetActive(false);

            /*
            if (isUnlock)
            {
                gameObject.SetActive(false);
                eventHandler.Success();
            }
            */
        }
    }

    public IEnumerator UnLockCor()
    {
        yield return new WaitForSeconds(2f);

        PlayerControl.instance.IsInteract = false;
        UIManager.instance.SetMenuState(false, false);

        isCurActive = false;

        SetMouseVisible(false);

        lockCam.SetActive(false);

        gameObject.SetActive(false);
        eventHandler.Success();
    }
}
