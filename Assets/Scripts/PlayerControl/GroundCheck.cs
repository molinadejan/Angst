using UnityEngine;

// 플레이어가 땅에 닿아있는지 확인하는 클래스입니다.
// PlayerControl 클래스의 IsGround 변수를 수정합니다.

public class GroundCheck : MonoBehaviour
{
    private PlayerControl pc;

    private void Awake()
    {
        pc = PlayerControl.instance;
    }

    // 콜라이더와 충돌중이면 땅에 닿아있는것으로 간주합니다.
    private void OnTriggerStay(Collider other)
    {
        if (((1 << other.gameObject.layer) & pc.terrainMask) != 0)
        {
            // 플레이어가 중력 변화 중이면 땅에 닿아있지 않은것으로 체크합니다.
            if (pc.IsRotate)
            {
                pc.IsGround = false;
                return;
            }

            if (pc.rb.velocity.y <= 0)
                pc.IsGround = true;
        }
    }

    // 콜라이더에서 나가면 땅에 닿아있지 않은것으로 간주합니다.
    private void OnTriggerExit(Collider other)
    {
        pc.IsGround = false;
    }
}
