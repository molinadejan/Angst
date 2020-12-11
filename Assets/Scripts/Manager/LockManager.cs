using UnityEngine;

public class LockManager : MonoBehaviour
{
    public LockInteract lockInteract;
    public AudioSource audioSource;

    public SlotButton[] slotButtons;

    public int numberCount;
    public float velocity = 50f;

    [HideInInspector] public float rotateAngle;

    private Animator anim;
    [SerializeField] private string answer = "";

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rotateAngle = 360 / numberCount;
    }

    public string MakePassword()
    {
        for(int i = 0; i < slotButtons.Length; i++)
        {
            answer += Random.Range(0, numberCount).ToString();
        }

        return answer;
    }

    public void SetPassword(string answer)
    {
        this.answer = answer;
    }

    public void AnswerCheck()
    {
        audioSource.Play();
        string lockNum = "";

        for(int i = 0; i < slotButtons.Length; i++)
        {
            if (slotButtons[i].moved) return;
            lockNum += (slotButtons[i].State).ToString();
        }

        if(answer.Equals(lockNum))
        {
            anim.SetBool("unLock", true);
            lockInteract.isUnlock = true;
            StartCoroutine(lockInteract.UnLockCor());
        }
    }
}
