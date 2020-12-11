/*
using DG.Tweening;
using UnityEngine;

public class Slotpart : MonoBehaviour
{
    public Transform[] slots;

    public Transform top;
    public Transform middle;
    public Transform bottom;

    public Figures state;

    private bool isclick = false;
    private int currenteNum = 0;

    private void Start()
    {
        state = Figures.circle;
    }

    public void ButtonClick()
    {
        if(!isclick) MoveImage();
    }

    private void MoveImage()
    {
        if (state == Figures.ddcircle) state = Figures.circle;
        else state++;

        int prev = currenteNum == 0 ? 3 : currenteNum - 1;
        int next = currenteNum == 3 ? 0 : currenteNum + 1;

        slots[prev].position = top.position;
        slots[next].DOMove(middle.position, 0.5f).SetUpdate(true);
        slots[currenteNum].DOMove(bottom.position, 0.5f).SetUpdate(true).OnStart(() => isclick = true).OnComplete(() =>
        {
            isclick = false;
            currenteNum = currenteNum == 3 ? 0 : currenteNum + 1;

            SlotManager.instance.Check();
        });
    }
}
*/
