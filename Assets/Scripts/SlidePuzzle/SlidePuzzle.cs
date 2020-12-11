#pragma warning disable CS0649

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// 슬라이드 퍼즐 초기화와 이동 정의 클래스

public class SlidePuzzle : MonoBehaviour , IPointerDownHandler,IPointerUpHandler
{
    private AudioSource audioSource;

    [SerializeField] private Image image;
    [SerializeField] private RectTransform rectTransform;

    [HideInInspector] public int positionY;
    [HideInInspector] public int positionX;
    [HideInInspector] public int num;

    [HideInInspector] public bool clicked;
    [HideInInspector] public bool moved;

    [HideInInspector] public bool go_left;
    [HideInInspector] public bool go_right;
    [HideInInspector] public bool go_up;
    [HideInInspector] public bool go_down;

    private void Awake()
    {
        audioSource = GetComponentInParent<AudioSource>();
    }

    private void Update()
    {
        PuzzleMove();
    }

    // 스프라이트 세팅
    public void SetSprite()
    {
        string filePath = "Sprites/UI/Puzzle" + "/HW-Logo-1-00" + (num);
        image.sprite = Resources.Load<Sprite>(filePath) as Sprite;
    }
    
    // 퍼즐 이동
    private void PuzzleMove()
    {
        if (go_up)
        {
            moved = true;
            go_up = false;
            rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, rectTransform.anchoredPosition.y + 101);
        }
        if (go_down)
        {
            moved = true;
            go_down = false;
            rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, rectTransform.anchoredPosition.y - 101);
        }
        if (go_left)
        {
            moved = true;
            go_left = false;
            rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x - 101, rectTransform.anchoredPosition.y);
        }
        if (go_right)
        {
            moved = true;
            go_right = false;
            rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x + 101, rectTransform.anchoredPosition.y);
        }
    }

    // 퍼즐 클릭 다운
    public void OnPointerDown(PointerEventData eventData)
    {
        if (IsPointerOverUIObject(Input.mousePosition))
        {
            clicked = true;
            audioSource.Play();
            SlidePuzzleManager.instance.MovePuzzle();
        }
    }

    // 퍼즐 클릭 업
    public void OnPointerUp(PointerEventData eventData)
    {
        if (IsPointerOverUIObject(Input.mousePosition))
        {
            clicked = false;
            moved = false;
        }
    }
    
    public bool IsPointerOverUIObject(Vector2 clickPos)
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);

        eventDataCurrentPosition.position = clickPos;

        List<RaycastResult> results = new List<RaycastResult>();

        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);

        return results.Count > 0;
    }
}
