using UnityEngine;

// 플레이어 점프후 착지 애니메이션 상태 클래스

public class PlayerJumpDown : PlayerStateBase
{
    private PlayerControl player;
    private bool check = true;

    public float transition;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (check)
        {
            check = false;
            player = GetPC(animator);
        }
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // 착지 후 바로 걸을지 가만히 있을지 애니메이션 선택

        if (InputManager.instance.hInput == 0 && InputManager.instance.vInput == 0)
            animator.SetBool(PlayerState.Walk.ToString(), false);
        else
            animator.SetBool(PlayerState.Walk.ToString(), true);

        player.SetCameraRotation();

        if (check) return;

        if (stateInfo.normalizedTime > transition)
        {
            animator.SetBool(PlayerState.Jump.ToString(), false);
            check = true;
            return;
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        check = true;

        // 점프의 끝을 알리는 변수 수정
        animator.SetBool(PlayerState.JumpStartToStay.ToString(), false);
        animator.SetBool(PlayerState.Jump.ToString(), false);

        player.IsJumping = false;
    }
}
