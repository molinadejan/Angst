using UnityEngine;

// 중력 변화시 공중에 떠있는 플레이어 애니메이션 상태 클래스

public class PlayerFloat : PlayerStateBase
{
    private PlayerControl player;
    private bool check = true;

    public float transition;
    public string fallDownSoundClipName;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (check)
        {
            check = false;
            player = GetPC(animator);
        }
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (check) return;

        // 추락 시 땅에 닿으면 땅과 부딪히는 애니메이션으로 전환합니다.
        if (stateInfo.normalizedTime > transition && player.IsGround)
        {
            player.rb.drag = 7;
            player.SoundPlay(fallDownSoundClipName, false);

            animator.SetBool(PlayerState.Falling.ToString(), false);

            PlayerStress.instance.Stress -= 20f;

            check = true;
            return;
        }
    }
}
