using UnityEngine;
using DG.Tweening;

// 중력 변화를 관리하는 클래스 입니다. 정확히는 맵을 알맞게 회전시키는 스크립트 입니다.
// 중력이 실제로 변하는게 아닌 플레이어를 기준으로 맵을 회전시켜 마치 중력이 변하는것 같은 착시를 줍니다.

public class GravityChangeManager : MonoBehaviour
{
    public static GravityChangeManager instance;

    private PlayerControl pc;

    // 맵 오브젝트
    public GameObject map;

    // mapPivot 을 중심으로 회전시킵니다.
    public Transform mapPivot;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            pc = FindObjectOfType<PlayerControl>();
        }
    }

    public void ChangeGravity(Vector3 rotateDir, Vector3 floatPos, float lookUpAngle)
    {
        // 각종 애니메이션 관련 변수들을 수정합니다.
        pc.playerAnimator.SetBool(PlayerState.Rotation.ToString(), true);
        pc.playerAnimator.SetBool(PlayerState.Falling.ToString(), true);
        pc.playerAnimator.SetBool(PlayerState.Jump.ToString(), false);
        pc.playerAnimator.SetBool(PlayerState.JumpStartToStay.ToString(), false);
        pc.playerAnimator.SetBool(PlayerState.JumpStayToDown.ToString(), false);
        pc.playerAnimator.SetBool(PlayerState.Walk.ToString(), false);

        // 중력이 변하는 동안은 플레이어에 작용하는 물리도 잠시 비활성화 해줍니다.
        pc.IsJumping = false;
        pc.rb.isKinematic = true;
        pc.rb.velocity = Vector3.zero;
        pc.rb.drag = 0;


        Sequence newSequence = DOTween.Sequence()
            // 플레이어를 공중에 띄웁니다
            .Append(pc.transform.DOMove(floatPos, 3f)
            .OnStart(() =>
            {
                pc.IsRotate = true;
            })
            .OnComplete(() =>
            {
                // mapPivot을 플레이어의 위치로 이동시킵니다
                // 이를 중심으로 맵이 회전하여 플레이어와 불필요한 충돌을 없앱니다
                mapPivot.transform.position = pc.transform.position;
                map.transform.SetParent(mapPivot);
            }))
            // 플레이어를 회전시킵니다. 특정 방향으로 플레이어를 낙하하게 합니다.
            .Append(pc.transform.DORotate(Vector3.up * lookUpAngle, 2f)
            .OnComplete(() =>
            {
                pc.MxAcc = lookUpAngle;
                pc.MyAcc = 0;
                pc.cams.transform.DOLocalRotateQuaternion(Quaternion.Euler(0, 0, 3), 3f);
            }))
            // 맵을 회전시킵니다.
            .Append(mapPivot.transform.DORotateQuaternion(Quaternion.Euler(rotateDir * 90), 5f))
            .OnComplete(() =>
            {
                map.transform.SetParent(null);
                mapPivot.eulerAngles = Vector3.zero;

                pc.rb.isKinematic = false;
                pc.IsRotate = false;
                pc.playerAnimator.SetBool(PlayerState.Rotation.ToString(), false);
            });
    }
}
