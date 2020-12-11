using UnityEngine;

// 스테이지 게임오버시 화면 연출에 쓰일 적 오브젝트를 관리하는 클래스
public class MotherEnemyScreen : MonoBehaviour
{
    public static MotherEnemyScreen instance = null;

    private Animator anim;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            anim = GetComponent<Animator>();
        }
    }

    // 공격 애니메이션 출력
    public void SetAttack()
    {
        anim.SetBool("Attack", true);
    }

    // 공격 사운드 출력
    public void PlayAttackSound()
    {
        MotherEnemy.instance.PlayVoiceAudio("MotherEnemyAttack", false);
    }
}
