using UnityEngine;

// 플레이어의 입력을 저장하는 변수들을 정의한 클래스 입니다.

public class InputManager : MonoBehaviour
{
    public static InputManager instance = null;

    // WASD
    [HideInInspector] public float hInput;
    [HideInInspector] public float vInput;

    // WASD Raw Input
    [HideInInspector] public float hInputRaw;
    [HideInInspector] public float vInputRaw;

    // Mouse Input
    [HideInInspector] public float mxInput;
    [HideInInspector] public float myInput;

    // Space bar
    [HideInInspector] public bool jumpAndClimb;
    
    // ESC
    [HideInInspector] public bool isCancle;

    // A
    [HideInInspector] public bool pageLeft;
    
    // R
    [HideInInspector] public bool pageRight;

    // E
    [HideInInspector] public bool interact;

    // 1
    [HideInInspector] public bool pillUse;
    
    // 2
    //[HideInInspector] public bool crossUse;

    // C
    [HideInInspector] public bool walkSlow;

    [HideInInspector] public bool lightTurnOnOff;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // 모든 입력을 막습니다. 게임 클리어나 게임 오버시 호출됩니다.
    public void AllInputBlock()
    {
        KeyboardInput.instance.enabled = false;

        hInput = vInput = hInputRaw = vInputRaw = mxInput = myInput = 0;
        jumpAndClimb = isCancle = pageLeft = pageRight = interact = pillUse /*= crossUse*/ = walkSlow = lightTurnOnOff = false;
    }
}
