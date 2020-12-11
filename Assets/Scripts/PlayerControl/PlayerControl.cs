#pragma warning disable CS0649

using DG.Tweening;
using System.Collections;
using UnityEngine;

public enum PlayerState
{
    Walk,
    Jump,
    JumpStartToStay,
    JumpStayToDown,
    Climb,
    Rotation,
    Falling,
    ClimbToStand,
    StandToIdle,
}

public class PlayerControl : MonoBehaviour, IEvent
{
    static public PlayerControl instance = null;

    // 플레이어 컨트롤 클래스 입니다.

    public Rigidbody rb;
    public CapsuleCollider cc;

    // 플레이어 사운드를 재생할 오디오 소스
    public AudioSource[] audioSource;

    // 플레이어의 중심 Transform
    public Transform center;

    // 올라갈곳을 확인할 레이를 쏘는 위치
    public Transform checkRayOrigin;

    // 땅으로 간주할 레이어 마스크
    public LayerMask terrainMask;

    // 올라갈수 있는 오브젝트의 레이어 마스크
    public LayerMask climbMask;
    public Animator playerAnimator;

    // 플레이어 카메라
    public GameObject cams;

    // 후레쉬 오브젝트
    public Light flashLight;

    // 플레이어 이동속도, 천천히 이동하는 속도, 마우스 회전 속도
    public float moveSpeed;
    public float moveSlowSpeed;
    public float rotationSpeed;

    // 점프 파워, 올라가기 체크 레이 길이
    public float jumpForce;
    public float checkRayDistance;

    // 카메라의 상하 최대 최소 각도
    public float maxAngle;
    public float minAngle;

    // 플레이어가 올라갈수 있는지 체크
    private bool canClimb;
    public bool CanClimb { set { canClimb = value; } get { return canClimb; } }

    // 플레이어가 땅에 있는지 체크
    private bool isGround;
    public bool IsGround { set { isGround = value; } get { return isGround; } }

    // 플레이어가 점프중인지 체크
    private bool isJumping;
    public bool IsJumping { set { isJumping = value; } get { return isJumping; } }

    // 플레이어가 올라가는 중인지 체크
    private bool isClimb;
    public bool IsClimb { set { isClimb = value; } get { return isClimb; } }

    // 플레이어가 중력 변화중인지 체크
    private bool isRotate;
    public bool IsRotate { set { isRotate = value; } get { return isRotate; } }

    // 현재 플레이어의 이동이 막혀있는지 체크
    private bool isMoveLock;
    public bool IsMoveLock { set { isMoveLock = value; } get { return isMoveLock; } }

    // 플레이어가 다른 오브젝트와 상호작용 중인지 체크
    private bool isInteract;
    public bool IsInteract { set { isInteract = value; } get { return isInteract; } }

    // 플레이어가 천천히 움직이고 있는지 체크
    private bool isMoveSlow;
    public bool IsMoveSlow { set { isMoveSlow = value; } get { return isMoveSlow; } }

    // 플레이어 좌우 회전값 누적
    private float mxAcc;
    public float MxAcc { get { return mxAcc; } set { mxAcc = value; } }

    // 플레이어 상하 회전값 누적
    private float myAcc;
    public float MyAcc { get { return myAcc; } set { myAcc = value; } }

    // 이동 방향
    private Vector3 dir;
    // 플레이어 회전 (좌우)
    private Vector3 playerRotation;
    // 카메라 회전 (상하)
    private Vector3 cameraRotation;

    // 올라가기 확인 레이가 충돌할경우 충돌체 정보 저장
    private RaycastHit climbHit;
    
    // 걷기. 천천히 걷기 속도값 저장용
    private float curMoveSpeed;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            curMoveSpeed = moveSpeed;
        }
    }

    private void Update()
    {
        // 랜턴을 켜고 끕니다.
        TurnOnOffLight();

        // 현재 공중에 있는지 확인하여 Drag 값을 정합니다.
        rb.drag = IsGround ? 7 : 0;

        #region 오직 UI 표시만을 위한 코드......
        canClimb = Physics.Raycast(checkRayOrigin.position, Vector3.down, checkRayDistance, climbMask);
        #endregion
    }

    #region Player Move, Rotate, Climb......

    // 라이트 컨트롤 함수
    public void TurnOnOffLight()
    {
        if (!IsMoveLock && !IsInteract && InputManager.instance.lightTurnOnOff)
        {
            SoundPlay("lightSwitch", false);
            flashLight.enabled = !flashLight.enabled;
        }
    }

    // 천천히 걷기
    public void SetMoveSlow()
    {
        isMoveSlow = !isMoveSlow;
        curMoveSpeed = isMoveSlow ? moveSlowSpeed : moveSpeed;
        UIManager.instance.moveSlow.gameObject.SetActive(isMoveSlow);
    }

    // 플레이어 이동
    public void SetDirAndMove()
    {
        if (IsMoveLock) return;

        dir.Set(InputManager.instance.hInputRaw, 0, InputManager.instance.vInputRaw);
        dir = transform.TransformDirection(dir);
        dir = dir.normalized * curMoveSpeed;
        dir.y += rb.velocity.y;

        rb.velocity = dir;
    }

    // 플레이어 회전 (좌우)
    public void SetPlayerRotation()
    {
        if (IsMoveLock) return;

        MxAcc += InputManager.instance.mxInput * rotationSpeed * Time.deltaTime;
        playerRotation.Set(0, MxAcc, 0);
        transform.eulerAngles = playerRotation;
    }

    // 카메라 회전 (상하)
    public void SetCameraRotation()
    {
        if (IsMoveLock) return;

        MyAcc += InputManager.instance.myInput * rotationSpeed * Time.deltaTime;
        MyAcc = Mathf.Clamp(MyAcc, minAngle, maxAngle);

        cameraRotation.Set(-MyAcc, 0, 3);

        cams.transform.localEulerAngles = cameraRotation;
    }

    // 올라가기
    public void ClimbMove()
    {
        transform.DOMove(new Vector3(transform.position.x, climbHit.point.y, transform.position.z), 1f)
            .SetDelay(0.2f)
            .OnComplete(() =>
            {
                transform.DOMove(climbHit.point, 0.5f);
            });
    }

    // 올라갈수 있는 곳인지 확인
    public bool CheckCanClimb()
    {
        return Physics.Raycast(checkRayOrigin.position, Vector3.down, out climbHit, checkRayDistance, climbMask);
    }

    // 떨어질때 높은곳인지 체크
    public float CheckRayBottom()
    {
        RaycastHit hit;

        Physics.Raycast(center.position, Vector3.down, out hit, 100, terrainMask);

        return hit.distance;
    }

    #endregion

    #region Player Sound Control

    // 현재 플레이어 오디오에서 재생중인지 체크
    public bool CheckSound()
    {
        for (int i = 0; i < audioSource.Length; i++)
            if (audioSource[i].isPlaying) return true;

        return false;
    }

    // 재생중이 아닌 오디오 소스 인덱스 가져오기
    private int GetAudioSource()
    {
        for(int i = 0; i < audioSource.Length; i++)
            if (!audioSource[i].isPlaying) 
                return i;

        return -1;
    }

    // 오디오 소스 재생, 클립 이름, 반복 여부 지정 가능
    public int SoundPlay(string clipName, bool isLoop)
    {
        int audioIndex = GetAudioSource();

        if(audioIndex != -1)
        {
            AudioSource audio = audioSource[audioIndex];

            audio.clip = AudioManager.instance.GetClip(clipName);
            audio.loop = isLoop;

            if (audio.clip != null) audio.Play();
        }

        return audioIndex;
    }

    private const string walkClipName = "PlayerWalk";
    private int stepIndex = 0;

    // 애니메이션 상에서 플레이어 발걸음 소리 출력
    public void PlayStepSound()
    {
        if (IsMoveSlow) return;

        int audioIndex = GetAudioSource();

        if(audioIndex != -1)
        {
            AudioSource stepAudio = audioSource[audioIndex];

            stepAudio.clip = AudioManager.instance.GetClip(walkClipName + "_" + stepIndex);
            stepIndex = (stepIndex + 1) % 4;

            stepAudio.Stop();
            stepAudio.Play();
        }
    }

    #endregion

    #region Player Input Lock for Seconds

    // 플레이어가 정해진 시간동안 움직이지 못하게 하기위해
    // IEvent 인터페이스를 상속, 구현

    public void EventPlay(float t)
    {
        StartCoroutine(EventPlayCor(t));
    }

    public void EventStop(float t)
    {
        throw new System.NotImplementedException();
    }

    public IEnumerator EventPlayCor(float t)
    {
        yield return null;

        IsMoveLock = true;

        yield return new WaitForSeconds(t);

        IsMoveLock = false;
    }

    public IEnumerator EventStopCor(float t)
    {
        throw new System.NotImplementedException();
    }

    #endregion

    #region NOT USE

    /*
    public void SoundAllStop()
    {
        for (int i = 0; i < audioSource.Length; i++) audioSource[i].Stop();
    }
    */

    /*
    public void SoundStop(int idx)
    {
        if (idx < 0 || idx >= audioSource.Length) return;
        if(audioSource[idx].isPlaying) audioSource[idx].Stop();
    }

    */

    /*
    private void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            Gizmos.color = canClimb ? Color.red : Color.green;

            Gizmos.DrawSphere(checkRayOrigin.position, 0.1f);
            Gizmos.DrawLine(checkRayOrigin.position, checkRayOrigin.position + Vector3.down * hitDistance);
        }
    }
    */

    #endregion
}
