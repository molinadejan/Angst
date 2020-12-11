using UnityEngine;

// 플레이어 올라가는 애니메이션 상태 클래스
public class PlayerClimb : PlayerStateBase
{
    private PlayerControl player;

    // 상태 전이 값
    public float transition;

    // 올라갈때 출력할 오디오 클립 이름
    public string climbSoundClipName;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GetPC(animator);

        // 불필요한 충돌방지를 위해 올라가는 동안은 중력의 영향을 받지않고
        // 콜라이더 또한 비활성화 합니다.
        player.rb.isKinematic = true;
        player.cc.enabled = false;
        player.IsClimb = true;

        player.ClimbMove();
        player.SoundPlay(climbSoundClipName, false);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // 올라가는 동안 카메라의 상하 회전은 가능합니다.
        player.SetCameraRotation();

        if (stateInfo.normalizedTime > transition)
        {
            animator.SetBool(PlayerState.ClimbToStand.ToString(), true);
            return;
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool(PlayerState.ClimbToStand.ToString(), false);
    }
}
