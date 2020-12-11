using UnityEngine;

// 스테이지2 인형 상호작용 클래스
public class DollInteract : Interactable
{
    private SphereCollider sc;
    private AudioSource audioSource;
    private Renderer myRenderer;

    public bool chaseCheck;
    public string clipName;

    private bool isActivate;

    protected override void Awake()
    {
        base.Awake();

        sc = GetComponent<SphereCollider>();
        audioSource = GetComponent<AudioSource>();
        myRenderer = GetComponent<Renderer>();
    }

    private void Start()
    {
        audioSource.clip = AudioManager.instance.GetClip(clipName);
    }

    private void Update()
    {
        if(!isActivate && isEnter && InputManager.instance.interact)
        {
            PlayerDoll.instance.GetDoll();
            UIManager.instance.PressTextSetFalse();
            UIManager.instance.SetGetText(description + " 획득");

            if(chaseCheck)
                MotherEnemy.instance.SetEnemyState(EnemyState.DollChase, transform);

            sc.enabled = false;
            myRenderer.enabled = false;

            audioSource.Play();
            isActivate = true;
        }

        if(isActivate && !audioSource.isPlaying)
        {
            gameObject.SetActive(false);
        }
    }
}
