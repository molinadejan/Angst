/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class Pipe : MonoBehaviour ,IPointerClickHandler  
{
    public Image image;

    private bool isactivated = false;
    private int HowManyTurn = 0;//얼만큼 돌아져 있는지 
    private int shape = 0;//1= straight, 2=curve
    public int Can_I_go = 0;//manage as bit mask  left = 1 , up =2 , right = 4, down = 8;
    public bool isFrame = true;

    bool rubber = false;

    Quaternion destinationrotation = new Quaternion();
    // Start is called before the first frame update
    void Awake()
    {
 
        image = gameObject.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, destinationrotation, 10f*Time.unscaledDeltaTime);
    }

    public void SetIsActivated(bool state)
    {
        isactivated = state;
    }
    public bool GetISActivated()
    {
        return isactivated;
    }
    public void SetImageState(int state)
    {
        gameObject.SetActive(true);
        string FilePath="";
        if (state == 1) {
            FilePath = "Wire/line";
            shape = 1;
        }else if (state == 2)
        {
            FilePath = "Wire/curvy";
            shape = 2;
        }else if (state == 3)
        {
            FilePath = "Wire/circle";
            shape = 3;
        }
        if(shape>=1 && shape <= 3)
        {
            isFrame = false;
        }
        image.sprite = Resources.Load<Sprite>(FilePath) as Sprite;
        HowManyTurn = Random.Range(0, 4);//방향 설정
        SetLineState(state, HowManyTurn);//방향을 기반으로 canigo(내가 갈 수 있는 방향들) 설정 
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isFrame)
        {
            HowManyTurn++;
            HowManyTurn %= 4;
            destinationrotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, -90 * HowManyTurn);
            SetLineState(shape, HowManyTurn);
        }
        //클릭시 고무장갑 없으면 스트레스 감소.
        rubber = WireManager.instance.isRubberGet;
        if (!rubber)
        {
            PlayerStress.instance.Stress -= 10f;
        }
       // Debug.Log(Can_I_go);
    }

    public int HOWManyTurn
    {
        get
        {
            return HowManyTurn;                                         
        }                                                       
        set 
        {
            HowManyTurn = value;
        }
    }

    public void StartEndImage(int cango)//시작,도착지 이미지는 항상 직선이다. 좌우 인지 상하인지 설정 
    {
        gameObject.SetActive(true);
        shape = 1;
        if (cango == 5)
        {
            HowManyTurn = 0;
        }
        else
        {
            HowManyTurn = 1;
        }
        Can_I_go = cango;
        destinationrotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, -90 * HowManyTurn);
        image.sprite = Resources.Load<Sprite>("Wire/tip") as Sprite;   
    }

    public void SetLineState(int shape,int trun)//4방향 0= 좌 2= 상 4= 우 8=하  -> 비트마스크이용 
    {
        destinationrotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, -90 * HowManyTurn);
        switch (shape)
        {
            case 1:
                trun %= 2;
                Can_I_go = (int)(Mathf.Pow(2, (trun%4)) + (int)Mathf.Pow(2, ((trun + 2)%4)));
                break;
            case 2:
                Can_I_go = (int)(Mathf.Pow(2, trun%4) + (int)Mathf.Pow(2, (trun + 1)%4));
                break;
            default:
                break;
        }
    }


    public int GetLineState()
    {
        return Can_I_go;
    }
    public int SetCanIGO
    {
        get
        {
            return Can_I_go;
        }
        set
        {
            Can_I_go = value;
        }
    }
}
*/