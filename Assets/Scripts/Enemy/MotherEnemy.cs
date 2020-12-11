using System.Collections;
using UnityEngine;
using UnityEngine.AI;

// 스테이지2 에 등장하는 적오브젝트 클래스 입니다.
// FSM으로 디자인되어 있습니다.

public enum EnemyState
{
    Idle,      // 대기
    Patrol,    // 정찰
    Chase,     // 추격
    Attak,     // 공격
    DollChase, // 인형 추적
    GameEnd,   // 게임오버
}

// 애니메이션 제어를 위한 열거형
public enum EnemyAnimState
{
    Idle,
    Walk,
    Attack,
}

public class MotherEnemy : MonoBehaviour
{
    public static MotherEnemy instance = null;

    // 길찾기 에이전트 클래스
    private NavMeshAgent navMeshAgent;
    private PlayerControl playerControl;

    private Transform playerControlTransform;
    private Transform enemyTransform;

    private Animator anim;

    // 적 목소리 오디오
    public AudioSource voiceAudio;

    // 적 발걸음 오디오
    public AudioSource stepAudio;

    // 플레이어까지 장애물 검출을 위한 레이어 마스크
    public LayerMask terrainMask;

    // 패트롤 포인트들
    public Transform[] patrolPoints;
    private int index = 1;

    // 최소 접근 거리
    public float approachDis;

    // 시야각
    public float sightAngle;
    [SerializeField] private bool checkSightAngle;

    // 단순 시야 거리
    public float sightDis;
    [SerializeField] private bool checkSightDis;

    // 소리를 감지하는 거리
    public float soundCatchDis;
    [SerializeField] private bool checkSoundCatchDis;

    // 추적을 유지하는 거리
    public float chaseDis;
    [SerializeField] private bool checkChaseDis;

    // 공격하는 거리
    public float attackDis;
    [SerializeField] private bool checkAttackDis;

    // 플레이어의 화면에 영향을 주는 거리
    // 미구현 상태로 남겨둡니다.
    /*
    public float effectDis;
    [SerializeField] private bool checkEffectDis;
    private bool isEffectOn;
    */

    // 오프메쉬링크에서의 멀티플라이어
    public float linkSpeedMultiplier;
    private bool isLink;

    // 스턴 시간
    //public float stunTime;

    // 현재 상태와 새 상태 감지
    private EnemyState curState;
    private bool isNewState;

    // 플레이어 벡터 - 적 벡터
    private Vector3 target;

    // 타겟 인형 벡터
    private Vector3 dollTarget;

    private float dis;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            navMeshAgent = GetComponent<NavMeshAgent>();
            playerControl = FindObjectOfType<PlayerControl>();
            playerControlTransform = playerControl.center;

            enemyTransform = GetComponent<Transform>();

            anim = GetComponent<Animator>();
        }
    }

    private void OnEnable()
    {
        enemyTransform.position = patrolPoints[0].position;
        navMeshAgent.enabled = true;

        approachDis   *= approachDis;
        sightDis      *= sightDis;
        soundCatchDis *= soundCatchDis;
        chaseDis      *= chaseDis;
        attackDis     *= attackDis;
        //effectDis     *= effectDis;

        curState = EnemyState.Patrol;

        StartCoroutine(MainCor());
    }

    private void Update()
    {
        target = playerControlTransform.position - enemyTransform.position;

        // 플레이어까지 거리를 측정합니다.
        #region Check distance from enemy to player

        dis = Vector3.SqrMagnitude(target);

        checkSightDis      = dis <= sightDis      ? true : false;
        checkSoundCatchDis = dis <= soundCatchDis ? true : false;
        checkChaseDis      = dis <= chaseDis      ? true : false;
        checkAttackDis     = dis <= attackDis     ? true : false;
        //checkEffectDis     = dis <= effectDis     ? true : false;

        #endregion

        // 플레이어와의 각도를 측정합니다.
        #region Check angle between enemy's forward and target

        target = target.normalized;

        float dot   = Vector3.Dot(enemyTransform.forward, target);
        float angle = Mathf.Acos(dot) * Mathf.Rad2Deg;

        checkSightAngle = angle <= sightAngle ? true : false;

        #endregion

        // 링크위에 있는지 체크합니다
        #region Check enemy is on link

        if (navMeshAgent.isOnOffMeshLink && !isLink)
        {
            isLink = true;
            navMeshAgent.speed *= linkSpeedMultiplier;
        }
        else if(!navMeshAgent.isOnOffMeshLink && isLink)
        {
            isLink = false;
            navMeshAgent.speed /= linkSpeedMultiplier;
        }

        #endregion

        // 플레이어 화면에 영향을 줍니다
        // URP 변경으로 인해 재 구현 필요
        // 미구현 상태로 남겨둡니다.
        
        #region Player Screen Effect Control
        /*
        if(checkEffectDis && !isEffectOn)
        {
            isEffectOn = true;
            ScreenEffects.instance.StartSBCCor();
        }    
        else if(!checkEffectDis && isEffectOn)
        {
            isEffectOn = false;
            ScreenEffects.instance.StopSBCCor();
            approachAudio.volume = 0;
        }

        if(isEffectOn)
        {
            float disRatio = 1 - Mathf.Sqrt(dis) / Mathf.Sqrt(effectDis);
            ScreenEffects.instance.sbcIntensity = disRatio * 100;
            ScreenEffects.instance.sbcShift     = disRatio * 1f;

            approachAudio.volume = approachAudioVolume * disRatio;
        }
        */
        #endregion

        // 적 사운드를 거리와 지형에 따라 조절합니다.
        // SoundWithWall 클래스로 구현
        #region Enemy Sound Control

        /*
        int wallCount = CheckTerrainCount(Mathf.Sqrt(dis));

        if(wallCount > 0)
        {
            if(wallCount >= 4)
            {
                voiceAudio.volume = voiceVolume / 2f;
                stepAudio.volume = stepVolume / 2f;
            }

            voiceFilter.cutoffFrequency = voiceCutOff;
            stepFilter.cutoffFrequency = stepCutOff;
        }
        else
        {
            voiceAudio.volume = voiceVolume;
            stepAudio.volume = stepVolume;

            voiceFilter.cutoffFrequency = DEFAULT_FILTER;
            stepFilter.cutoffFrequency = DEFAULT_FILTER;
        }
        */

        #endregion
    }

    // 적의 상태를 설정합니다.
    public void SetEnemyState(EnemyState newState, Transform target = null)
    {
        if (newState == EnemyState.DollChase)
            dollTarget = target.position;

        isNewState = true;
        curState = newState;
    }

    // 추적 조건을 체크합니다.
    private void ChaseConditionCheck()
    {
        if (CheckSafety.instance.isInSafety) return;

        // 시야가 짧다
        // 시야 거리, 시야각 내에 플레이어가 있고 사이에 장애물이 없으면 추적합니다.
        if (checkSightDis && checkSightAngle && !CheckTerrain(Mathf.Sqrt(dis)))
        {
            SetEnemyState(EnemyState.Chase);
        }

        // 청각이 예민하다
        // 사운드 거리 내에 플레이어가 있고 플레이어가 소리내는 중이면 추적합니다
        else if (checkSoundCatchDis && PlayerControl.instance.CheckSound())
        {
            SetEnemyState(EnemyState.Chase);
        }
    }

    // 목소리 오디오를 재생합니다.
    public void PlayVoiceAudio(string clipName, bool loop)
    {
        voiceAudio.Stop();
        voiceAudio.clip = AudioManager.instance.GetClip(clipName);
        voiceAudio.loop = loop;
        voiceAudio.Play();
    }

    // FSM 메인 코루틴
    private IEnumerator MainCor()
    {
        while(true)
        {
            isNewState = false;
            yield return StartCoroutine(curState.ToString());
        }
    }

    // 대기 상태
    private IEnumerator Idle()
    {
        print("Enemy Idle");

        anim.SetBool(EnemyAnimState.Walk.ToString(), false);

        float timer = 0;
        float timeLimit = Random.Range(5f, 9f);

        do
        {
            timer += Time.deltaTime;

            if(timer >= timeLimit)
            {
                SetEnemyState(EnemyState.Patrol);
            }
            else
            {
                // 추적 조건을 판단합니다.
                ChaseConditionCheck();
            }

            yield return null;

            if (isNewState)
                break;

        } while (!isNewState);
    }

    // 정찰 상태
    private IEnumerator Patrol()
    {
        print("Enemy Patrol");

        anim.SetBool(EnemyAnimState.Walk.ToString(), true);

        int counter = 0;
        int counterLimit = Random.Range(5, 10);

        do
        {
            // 패트롤 포인트에 근접하면 다음 포인트로 인덱스를 변경합니다.
            if (Vector3.SqrMagnitude(patrolPoints[index].position - enemyTransform.position) <= approachDis)
            {
                index = (index == patrolPoints.Length - 1) ? 0 : index + 1;
                ++counter;
            }

            // 일정 카운터 이상이면 잠시 대기합니다.
            if (counter >= counterLimit)
                SetEnemyState(EnemyState.Idle);
            else
            {
                // 패트롤 포인트로 이동지점을 설정합니다.
                navMeshAgent.SetDestination(patrolPoints[index].position);

                // 추적 조건을 판단합니다.
                ChaseConditionCheck();
            }

            yield return null;

            if (isNewState) 
                break;

        } while (!isNewState);
    }

    // 추적 상태
    private IEnumerator Chase()
    {
        print("Enemy Chase");
        
        anim.SetBool(EnemyAnimState.Walk.ToString(), true);

        do
        {
            // 추적 거리 이내면 추적
            if (checkChaseDis)
            {
                navMeshAgent.SetDestination(playerControlTransform.position);
            }
            // 아니면 정찰로 복귀
            else
            {
                SetEnemyState(EnemyState.Patrol);
            }

            // 공격거리 이내이고 장애물이 없다면 공격
            // 한 방 맞으면 게임오버됩니다.
            if (checkAttackDis && !CheckTerrain(Mathf.Sqrt(dis)))
            {
                PlayerStress.instance.Stress -= 200f;
                SetEnemyState(EnemyState.GameEnd);
            }

            yield return null;

            if (isNewState)
                break;

        } while (!isNewState);
    }

    // 플레이어가 인형 획득 시 그 위치로 이동합니다.
    private IEnumerator DollChase()
    {
        print("DollChase");

        anim.SetBool(EnemyAnimState.Walk.ToString(), true);

        do
        {
            // 특정 거리 이내 접근하면 대기상태로 전환
            if (Vector3.SqrMagnitude(enemyTransform.position - dollTarget) <= approachDis)
            {
                SetEnemyState(EnemyState.Idle);
            }
            else
            {
                navMeshAgent.SetDestination(dollTarget);

                // 추적 조건을 판단합니다.
                ChaseConditionCheck();
            }

            yield return null;

            if (isNewState)
                break;

        } while (!isNewState);
    }

    // 게임 끝, 의미없는 클래스입니다.
    private IEnumerator GameEnd()
    {
        print("Game End");

        anim.SetBool(EnemyAnimState.Walk.ToString(), false);

        do
        {
            yield return null;
        } while (!isNewState);
    }

    
    // 플레이어까지 레이를 쏘아 장애물을 확인합니다.
    private bool CheckTerrain(float dis)
    {
        return Physics.Raycast(enemyTransform.position, target, dis, terrainMask);
    }

    private const string walkClipName = "MotherEnemyWalking";

    // 애니메이션에 맞추어 발걸음을 출력합니다.
    public void PlayStepSound()
    {
        int index = Random.Range(0, 4);

        stepAudio.clip = AudioManager.instance.GetClip(walkClipName + "_" + index);

        stepAudio.Stop();
        stepAudio.Play();
    }

    // 에디터 디버그용
    /*
    public void OnDrawGizmos()
    {
        if (curState == EnemyState.Patrol)
            Gizmos.color = Color.green;
        else
            Gizmos.color = Color.red;

        Vector3 right = Quaternion.Euler(0f,  sightAngle, 0f) * enemyTransform.forward;
        Vector3 left  = Quaternion.Euler(0f, -sightAngle, 0f) * enemyTransform.forward;

        Gizmos.DrawLine(enemyTransform.position, enemyTransform.position + enemyTransform.forward * 10);
        Gizmos.DrawLine(enemyTransform.position, enemyTransform.position + right * 10);
        Gizmos.DrawLine(enemyTransform.position, enemyTransform.position + left * 10);
    }
    */

    #region NOT USE

    /*
    private IEnumerator Stun()
    {
        //print("Enemy Stun");

        anim.SetBool(EnemyAnimState.Walk.ToString(), false);

        float stunTimer = 0;
        navMeshAgent.isStopped = true;

        do
        {
            stunTimer += Time.deltaTime;

            if (stunTimer >= stunTime)
                SetEnemyState(EnemyState.Chase);

            yield return null;

            if (isNewState)
                break;

        } while (!isNewState);

        navMeshAgent.isStopped = false;
    }
    */

    #endregion
}

