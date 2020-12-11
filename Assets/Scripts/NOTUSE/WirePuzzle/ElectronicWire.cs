/*
using UnityEngine;

public class ElectronicWire : Interactable
{
    public GameObject WirePuzzle;
    public Moving WPuzzle;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {

        if (InputManager.instance.interact && isEnter && !isCurActive) {
            UIManager.instance.PressTextSetFalse();
            PlayerControl.instance.isInteract = true;
            UIManager.instance.SetMenuState(true);
            isCurActive = true;
            SetTimescale(0f);
            SetMouseVisible(true);
            WirePuzzle.SetActive(true);
        }
        if(isCurActive && (InputManager.instance.isCancle || WPuzzle.ComPlete))
        {
            PlayerControl.instance.isInteract = false;
            UIManager.instance.SetMenuState(false);
            if (WPuzzle.ComPlete)
            {
                WireManager.instance.isPuzzleClear += 1 << WPuzzle.PuzzleNumber;
            }
            isCurActive = false;
            SetTimescale(1f);
            SetMouseVisible(false);
            WirePuzzle.SetActive(false);
        }
    }
}
*/
