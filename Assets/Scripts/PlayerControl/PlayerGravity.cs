using UnityEngine;

// 플레이어에게 계속 중력을 가하는 클래스입니다.
// 리지드 바디 컴포넌트를 사용하고 있지만 좀더 자연스러운 중력 연출을 위해 별도로 구현하였습니다.

public class PlayerGravity : MonoBehaviour
{
    private const float GRAVITY = -9.8f;

    // 기본 중력에 곱하여 더 강한 중력을 만드는 계수입니다.
    public float playerGravityMultiple;


    private Rigidbody rb;
    private PlayerControl pc;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        pc = GetComponent<PlayerControl>();
    }

    private void FixedUpdate()
    {
        Gravity();
    }

    private void Gravity()
    {
        // 플레이어가 어디 올라가는 중이 아니라면 계속 중력을 가합니다.
        if(!pc.IsClimb)
        {
            rb.AddForce(Vector3.up * GRAVITY * playerGravityMultiple, ForceMode.Force);
        }
    }
}
