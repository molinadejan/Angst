using UnityEngine;

// 플레이어 점프 시작 애니메이션 상태 스크립트

public class PlayerJumpStart : PlayerStateBase
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

            // 점프 힘을 가합니다.
            player.IsJumping = true;
            player.rb.AddForce(Vector3.up * GetPC(animator).jumpForce, ForceMode.Impulse);
        }
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player.SetCameraRotation();
        player.SetPlayerRotation();

        if (check) return;

        // 점프 중일때도 올라갈수 있다면 올라가기로 전이
        if (InputManager.instance.jumpAndClimb && player.CheckCanClimb() && !player.IsClimb)
        {
            player.IsJumping = false;

            animator.SetBool(PlayerState.Climb.ToString(), true);
            animator.SetBool(PlayerState.Jump.ToString(), false);

            check = true;
            return;
        }

        if (stateInfo.normalizedTime > transition)
        {
            animator.SetBool(PlayerState.JumpStartToStay.ToString(), true);
            check = true;
            return;
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        check = true;
        animator.SetBool(PlayerState.JumpStartToStay.ToString(), false);
    }
}
