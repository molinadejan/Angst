/*
using UnityEngine;

public class SlotInteract : Interactable
{
    public GameObject slotUI;

    private void Update()
    {
        if(!isCurActive && InputManager.instance.interact && isEnter)
        {
            UIManager.instance.PressTextSetFalse();
            PlayerControl.instance.isInteract = true;

            isCurActive = true;

            SetMouseVisible(true);

            SetTimescale(0f);

            slotUI.SetActive(true);
            UIManager.instance.SetMenuState(true);
        }
        
        if(isCurActive && InputManager.instance.isCancle)
        {
            PlayerControl.instance.isInteract = false;

            isCurActive = false;

            SetMouseVisible(false);

            SetTimescale(1f);

            slotUI.SetActive(false);
            UIManager.instance.SetMenuState(false);
        }
    }
}
*/
