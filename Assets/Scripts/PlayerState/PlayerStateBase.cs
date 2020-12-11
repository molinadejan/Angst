using UnityEngine;

// 플레이어의 모든 애니메이션 상태 클래스는 이 클래스를 상속받아 PlayerControl 클래스에 접근 가능합니다.

public class PlayerStateBase : StateMachineBehaviour
{
    private PlayerControl pc;

    public PlayerControl GetPC(Animator anim)
    {
        if(pc == null)
        {
            pc = anim.GetComponentInParent<PlayerControl>();
        }

        return pc;
    }
}
