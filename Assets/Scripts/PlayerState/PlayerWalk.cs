using UnityEngine;

// 플레이어 걷기 애니메이션 상태 클래스
public class PlayerWalk : PlayerStateBase
{
    private PlayerControl player;

    public string walkSpeedParameterName;

    public float normalMultiplier;
    public float slowMultiplier;

    // 점프 후 착지로 전환하는 높이
    public float fallDownDis;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GetPC(animator);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // 천천히 걷기
        if (InputManager.instance.walkSlow)
        {
            player.SetMoveSlow();
            animator.SetFloat(walkSpeedParameterName, player.IsMoveSlow ? slowMultiplier : normalMultiplier);
        }

        // 대기상태
        if ((InputManager.instance.hInput == 0 && InputManager.instance.vInput == 0) || player.IsMoveLock)
        {
            animator.SetBool(PlayerState.Walk.ToString(), false);
            return;
        }
        
        // 올라가기
        if (!player.IsMoveLock && InputManager.instance.jumpAndClimb && player.CheckCanClimb() && !player.IsClimb)
        {
            animator.SetBool(PlayerState.Climb.ToString(), true);
            return;
        }

        // 점프
        if (!player.IsMoveLock && InputManager.instance.jumpAndClimb && !player.IsClimb && !player.IsJumping && /*!player.IsOnAir*/ player.IsGround)
        {
            animator.SetBool(PlayerState.Jump.ToString(), true);
            return;
        }

        // 높은곳에서 떨어지기
        if (player.CheckRayBottom() >= fallDownDis)
        {
            animator.SetBool(PlayerState.JumpStartToStay.ToString(), true);
            return;
        }

        player.SetDirAndMove();
        player.SetCameraRotation();
        player.SetPlayerRotation();
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool(PlayerState.Walk.ToString(), false);
    }
}
