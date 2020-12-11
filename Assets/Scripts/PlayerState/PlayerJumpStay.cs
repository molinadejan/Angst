using UnityEngine;

// 플레이어 점프 중 애니메이션 상태 클래스
public class PlayerJumpStay : PlayerStateBase
{
    private PlayerControl player;
    private bool check = true;

    public float transition;
    public string jumpDownSoundClipName;

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
        player.SetCameraRotation();
        player.SetPlayerRotation();

        if (check) return;

        // 점프중에도 어딘가에 올라갈수 있습니다.
        if (InputManager.instance.jumpAndClimb && player.CheckCanClimb() && !player.IsClimb)
        {
            player.IsJumping = false;

            animator.SetBool(PlayerState.Climb.ToString(), true);
            animator.SetBool(PlayerState.Jump.ToString(), false);

            check = true;
            return;
        }

        // 만약에 땅에 닿았다면 착지 상태로 전이합니다.
        if (stateInfo.normalizedTime > transition && player.IsGround)
        {
            player.SoundPlay(jumpDownSoundClipName, false);
            animator.SetBool(PlayerState.JumpStayToDown.ToString(), true);

            check = true;
            return;
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        check = true;
        animator.SetBool(PlayerState.JumpStayToDown.ToString(), false);
    }
}
