using UnityEngine;

// 올라가기 이후 제자리에서 서는 애니메이션 상태 클래스

public class PlayerStandToIdle_C : PlayerStateBase
{
    private PlayerControl player;
    public float transition;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GetPC(animator);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (stateInfo.normalizedTime > transition)
        {
            animator.SetBool(PlayerState.StandToIdle.ToString(), true);
        }

        player.SetCameraRotation();
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool(PlayerState.StandToIdle.ToString(), false);
        animator.SetBool(PlayerState.Climb.ToString(), false);

        // 올라가기가 끝났으니 중력을 다시 활성화 하고 콜라이더도 활성화 합니다.
        player.IsClimb = false;
        player.rb.isKinematic = false;
        player.cc.enabled = true;
    }
}
