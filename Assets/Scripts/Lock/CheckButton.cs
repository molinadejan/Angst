using UnityEngine;

public class CheckButton : MonoBehaviour
{
    public LockManager lockManager;

    public void OnMouseUp()
    {
        lockManager.AnswerCheck();
    }
}
