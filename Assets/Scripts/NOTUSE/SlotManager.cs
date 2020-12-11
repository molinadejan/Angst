/*
using UnityEngine;

public enum Figures
{
    circle,
    tri,
    square,
    ddcircle
}

public class SlotManager : MonoBehaviour
{
    public static SlotManager instance = null;

    public EventHandlerWithPuzzle events;

    public GameObject slotMachine;
    public GameObject UIButtons;
    public Slotpart[] slots;
    public Figures[] answer;

    private int index;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void SetPassword(int num)
    {
        if (index < answer.Length)
        {
            answer[index] = (Figures)num;
            index++;
        }
    }

    public void Check()
    {
        for(int i = 0; i < answer.Length; i++)
            if(slots[i].state != answer[i])
                return;

        UIButtons.SetActive(false);
        events.Success();
    }
}
*/
