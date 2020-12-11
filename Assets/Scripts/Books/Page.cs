using UnityEngine;

public class Page : MonoBehaviour
{
    public float velocityX  = 1f;
    public float smoothtime = 1f;
    public float speed      = 1f;

    private const float MAX_SPEED = 50000f;

    [HideInInspector] public bool moved = false;
    [HideInInspector] public bool left  = false;
    [HideInInspector] public bool right =  true;

    private Quaternion destinationQua;
    private Vector3 destinationV;

    public GameObject BackGround;
    public GameObject panel;

    [HideInInspector] public Texts texts;

    /// 페이지 이동시의 속도, 걸릴때 까지의 시간등을 설정합니다.
    /// unscaledTimeScale을 사용하기 위해 maxspeed를 설정합니다.
    /// book 스크립트에서 사용하기 위한 변수 move,left right를 설정하고
    /// 페이지의 목적지와 각도 
    /// 그리고 배경과 텍스트를 선언했습니다.

    private void Awake()
    {
        destinationQua = transform.rotation;
        destinationV = transform.position;
        texts = GetComponent<Texts>();
    }

    private void Start()
    {
        texts.SetRightTexts();
    }

    private void Update()
    {
        MoveToPage(destinationV, destinationQua, speed);

        if (transform.eulerAngles.y >= 86 && transform.eulerAngles.y <= 94)
        {
            transform.SetAsLastSibling();

            if (right)
            {
                texts.SetRightTexts();
                TurnLeftPanel();
            }
            else if (left)
            {
                texts.SetLeftTexts();
                TurnRightPanel();
            }
        }

        //일정 각도로 들어오면 텍스트와 이미지를 회전시키고 보여지는 텍스트들을 알맞은 것으로 변경시킵니다.
    }

    public void SetDestination(Vector3 DestiV, Quaternion DestiQ)
    {
        if ((transform.eulerAngles.y < 10 && transform.eulerAngles.y >= 0) || (transform.eulerAngles.y <= 180 && transform.eulerAngles.y > 170))
        {
            destinationV = DestiV;
            destinationQua = DestiQ;
            moved = true;
        }

        //book 스크립트에서 받아온 쿼터니언과 벡터 값을 설정합니다. 그 후, 움직일 수 있다고 설정합니다
    }

    void MoveToPage(Vector3 DestinationVector, Quaternion DestinationQua, float speed)
    {
        // 560 1360
        // Vector3 yoffset = new Vector3(0, transform.position.y, 0);
        // float fracComplete = (Time.deltaTime - StartTime) *speed;

        float goX = Mathf.SmoothDamp(transform.position.x, DestinationVector.x, ref velocityX, smoothtime, MAX_SPEED, Time.unscaledDeltaTime);
        transform.position = new Vector3(goX, transform.position.y, transform.position.z);

        // transform.position = Vector3.Slerp(transform.position, DestinationVector, fracComplete );
        // transform.position += yoffset;

        transform.rotation = Quaternion.Slerp(transform.rotation, DestinationQua, speed * Time.unscaledDeltaTime);

        //페이지는 선형적인 속도가 아닌 구면선형보간의 속도로 움직 일 수 있는 slerp함수와 smoothDamp함수를 사용했습니다.
        //목적지 까지 지정된 시간 이내에 도착합니다.
    }


    ///페이지가 회전할 때 내부의 이미지들도 함께 회전하게 되는데
    ///이렇게 될 경우 모든것이 좌우 반전이 되어 알아 볼 수 없게됩니다.
    ///그래서 이를 방지하기 위해 텍스트와 이미지들을 반전시켜줍니다.

    public void TurnLeftPanel()
    {
        panel.transform.eulerAngles = new Vector3(0, 90f, 0);
    }

    public void TurnRightPanel()
    {
        panel.transform.eulerAngles = new Vector3(0, -90f, 0);
    }
}
