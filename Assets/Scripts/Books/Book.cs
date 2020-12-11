using System.Collections.Generic;
using UnityEngine;

public class Book : MonoBehaviour
{
    public static Book instance = null;

    public List<Page> pages;

    public Transform startPoint;
    public Transform desPoint;

    private int curLPageNum  = -1;
    private int curRPageNum =  0;
    private Vector3 desVector;
    private Quaternion desQua;

    /// 각 페이지의 정보를 저장하는 pages list
    /// 오른쪽, 왼쪽의 목표지점 위치를 선언
    /// 현재 왼쪽 페이지와 오른쪽 페이지의 페이지 번호를 저장하고
    /// 페이지 이동을 위한 백터와 쿼터니언을 선언합니다.

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    private void Update()
    {
        if (InputManager.instance.pageLeft) GotoLeft();
        else if (InputManager.instance.pageRight) GotoRight();
    }

    /// 페이지를 좌, 우로 움직이기 위한 함수입니다.
    /// 위의 두 함수는 기능적으로 같으며 단지 방향만이 다를 뿐 입니다.
    /// 우선 목표 지점의 위치정보와 각도를 오일러 값으로 받아온 후
    /// 해당 지점으로 이동하기 위해 각 page에 존재하는 쿼터니언과 벡터에 그 값을 전달합니다.
    /// 그 후, 페이지가 움직였다면 페이지의 번호를 조정해주고 해당 페이지의 위치를 조절합니다.
    
    private void GotoLeft() 
    {
        if (curRPageNum < pages.Count)
        {
            desVector = new Vector3
                (
                    desPoint.position.x,
                    pages[curRPageNum].transform.position.y,
                    pages[curRPageNum].transform.position.z
                );

            desQua = Quaternion.Euler(0, -180f, 0);

            pages[curRPageNum].SetDestination(desVector, desQua);

            if (pages[curRPageNum].moved)
            {
                UIManager.instance.PlayUISound("book");
                curLPageNum = curRPageNum;
                pages[curLPageNum].moved = false;
                pages[curLPageNum].left = true;
                pages[curLPageNum].right = false;
                curRPageNum++;
            }
        }
    }

    // 페이지를 오른쪽으로 넘깁니다.
    private void GotoRight()
    {
        if (curLPageNum >= 0)
        {
            desVector = new Vector3
                (
                    startPoint.position.x,
                    pages[curLPageNum].transform.position.y,
                    pages[curLPageNum].transform.position.z
                );

            desQua = Quaternion.Euler(0, 0, 0);

            pages[curLPageNum].SetDestination(desVector, desQua);

            if (pages[curLPageNum].moved)
            {
                UIManager.instance.PlayUISound("book");
                curRPageNum = curLPageNum;
                pages[curRPageNum].moved = false;
                pages[curRPageNum].left = false;
                pages[curRPageNum].right = true;
                curLPageNum--;
            }
        }
    }
}
