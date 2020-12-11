using UnityEngine;

// 플레이어 대기 애니메이션 상태 클래스

public class PlayerIdle : PlayerStateBase
{
    private PlayerControl player;

    // 이 거리 이상의 높이에서 떨어지면 점프 후 착지 애니메이션으로 전환합니다.
    public float fallDownDis;

    public string walkSpeedParameterName;

    // 보통 걷기시 애니메이션 재생 속도 입니다
    public float normalMultiplier;

    // 천천히 걷기 시 애니메이션 재생 속도 입니다.
    public float slowMultiplier;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GetPC(animator);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (player.IsMoveLock) return;

        // 천천히 걷기
        if (InputManager.instance.walkSlow)
        {
            player.SetMoveSlow();
            animator.SetFloat(walkSpeedParameterName, player.IsMoveSlow ? slowMultiplier : normalMultiplier);
        }

        // 걷기 체크
        if (!player.IsMoveLock && InputManager.instance.hInput != 0 || InputManager.instance.vInput != 0)
        {
            animator.SetBool(PlayerState.Walk.ToString(), true);
            return;
        }

        // 올라가기 체크
        if (!player.IsMoveLock && InputManager.instance.jumpAndClimb && player.CheckCanClimb() && !player.IsClimb)
        {
            animator.SetBool(PlayerState.Climb.ToString(), true);
            return;
        }

        // 점프 체크
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

        player.SetPlayerRotation();
        player.SetCameraRotation();
    }
}
