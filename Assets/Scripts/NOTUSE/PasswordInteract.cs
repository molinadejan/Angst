/*
using UnityEngine;

public class PasswordInteract : Interactable
{
    public GameObject passwordUI;

    private void Update()
    {
        if(!isCurActive && isEnter && InputManager.instance.interact)
        {
            UIManager.instance.PressTextSetFalse();

            PlayerControl.instance.isInteract = true;
            UIManager.instance.SetMenuState(true);

            isCurActive = true;

            SetMouseVisible(true);

            SetTimescale(0f);

            passwordUI.SetActive(true);
        }

        if(isCurActive && InputManager.instance.isCancle)
        {
            PlayerControl.instance.isInteract = false;
            UIManager.instance.SetMenuState(false);

            isCurActive = false;

            SetMouseVisible(false);

            SetTimescale(1f);

            passwordUI.SetActive(false);
        }
    }
}
*/
