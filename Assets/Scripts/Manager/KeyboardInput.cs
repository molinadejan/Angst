using UnityEngine;

// 키보드 입력을 받아 각종 변수에 저장하는 클래스 입니다.

public class KeyboardInput : MonoBehaviour
{
    public static KeyboardInput instance = null;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    private void Update()
    {
        InputManager.instance.hInput = Input.GetAxis("Horizontal");
        InputManager.instance.vInput = Input.GetAxis("Vertical");

        if (InputManager.instance.hInput > 0)
            InputManager.instance.hInputRaw = 1;
        else if (InputManager.instance.hInput == 0)
            InputManager.instance.hInputRaw = 0;
        else
            InputManager.instance.hInputRaw = -1;

        if (InputManager.instance.vInput > 0)
            InputManager.instance.vInputRaw = 1;
        else if (InputManager.instance.vInput == 0)
            InputManager.instance.vInputRaw = 0;
        else
            InputManager.instance.vInputRaw = -1;

        InputManager.instance.mxInput = Input.GetAxis("Mouse X");
        InputManager.instance.myInput = Input.GetAxis("Mouse Y");

        if(Input.GetKeyDown(KeyCode.Space))
        {
            InputManager.instance.jumpAndClimb = true;
        }
        else
        {
            InputManager.instance.jumpAndClimb = false;
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            InputManager.instance.isCancle = true;
        }
        else
        {
            InputManager.instance.isCancle = false;
        }

        if(Input.GetKeyDown(KeyCode.D))
        {
            InputManager.instance.pageLeft = true;
            InputManager.instance.pageRight = false;
        }
        else
        {
            InputManager.instance.pageLeft = false;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            InputManager.instance.pageRight = true;
            InputManager.instance.pageLeft = false;
        }
        else
        {
            InputManager.instance.pageRight = false;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            InputManager.instance.interact = true;
        }
        else
        {
            InputManager.instance.interact = false;
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            InputManager.instance.walkSlow = true;
        }
        else
        {
            InputManager.instance.walkSlow = false;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            InputManager.instance.pillUse = true;
        }
        else
        {
            InputManager.instance.pillUse = false;
        }

        if(Input.GetMouseButtonDown(0))
        {
            InputManager.instance.lightTurnOnOff = true;
        }
        else
        {
            InputManager.instance.lightTurnOnOff = false;
        }

        /*
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            InputManager.instance.crossUse = true;
        }
        else
        {
            InputManager.instance.crossUse = false;
        }
        */
    }
}
