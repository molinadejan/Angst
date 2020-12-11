using System.Collections.Generic;
using UnityEngine;

// 스테이지1 슬라이드 퍼즐을 관리하는 클래스 입니다.

public class SlidePuzzleManager : MonoBehaviour
{
    public static SlidePuzzleManager instance = null;

    public SlidePuzzle[] puzzleList;

    private int[,] puzzleArr = new int[3, 3];
    private List<int> randomNumbers = new List<int>();

    /// 각 퍼즐의 정보를 받는 SlidePuzzle과
    /// 퍼즐들의 위치를 알아내기 위한 puzzleArr 배열
    /// 그리고 랜덤정답을 생성하기 위한 randomNumbers를 선언.

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        SetPuzzle();
        CorrectPuzzle();
    }

    private void SetPuzzle()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                puzzleArr[i, j] = i * 3 + j + 1;
                puzzleList[i * 3 + j].positionY = i;
                puzzleList[i * 3 + j].positionX = j;
            }
        }
    }

    /// 퍼즐을 섞습니다. 이 과정에서 '풀 수 없는 퍼즐'이 나올 수 있기 때문에
    /// 퍼즐의 무질서도를 검사하여 짝수가 될 때까지 계속해서 진행합니다.
    private void MixPuzzle()
    {
        randomNumbers.Add(7);
        puzzleList[6].num = 7;

        for (int i = 0; i < 9; i++)
        {
            if (i == 6)
            {
                continue;
            }

            int number = Random.Range(1, 10);

            while (randomNumbers.Contains(number))
            {
                number = Random.Range(1, 10);
            }

            randomNumbers.Add(number);
            puzzleList[i].num = number;
        }
    }

    //퍼즐의 그림들을 설정합니다.
    private void SetSprite()
    {
        foreach (SlidePuzzle p in puzzleList)
        {
            p.SetSprite();
        }
    }

    //퍼즐을 움직입니다. 배열 상에서는 위치를 바꿔주고 시각적인 이동은 puzzle의 활성화된 인자에 따라 이동합니다
    public void MovePuzzle()
    {
        foreach (SlidePuzzle puzzle in puzzleList)
        {
            int y = puzzle.positionY;
            int x = puzzle.positionX;

            if (puzzle.clicked)
            {
                if (y > 0 && puzzleArr[y - 1, x] == 7 && puzzle.moved == false)
                {
                    puzzle.go_up = true;

                    Swap(ref puzzle.positionX, ref puzzleList[6].positionX);
                    Swap(ref puzzle.positionY, ref puzzleList[6].positionY);
                    Swap(ref puzzleArr[y, x], ref puzzleArr[y - 1, x]);
                }

                if (y < 2 && puzzleArr[y + 1, x] == 7 && puzzle.moved == false)
                {
                    puzzle.go_down = true;

                    Swap(ref puzzle.positionX, ref puzzleList[6].positionX);
                    Swap(ref puzzle.positionY, ref puzzleList[6].positionY);
                    Swap(ref puzzleArr[y, x], ref puzzleArr[y + 1, x]);
                }

                if (x > 0 && puzzleArr[y, x - 1] == 7 && puzzle.moved == false)
                {
                    puzzle.go_left = true;

                    Swap(ref puzzle.positionX, ref puzzleList[6].positionX);
                    Swap(ref puzzle.positionY, ref puzzleList[6].positionY);
                    Swap(ref puzzleArr[y, x], ref puzzleArr[y, x - 1]);
                }

                if (x < 2 && puzzleArr[y, x + 1] == 7 && puzzle.moved == false)
                {
                    puzzle.go_right = true;

                    Swap(ref puzzle.positionX, ref puzzleList[6].positionX);
                    Swap(ref puzzle.positionY, ref puzzleList[6].positionY);
                    Swap(ref puzzleArr[y, x], ref puzzleArr[y, x + 1]);
                }
            }
        }
    }

    private void Swap(ref int a, ref int b)
    {
        int temp = a;
        a = b;
        b = temp;
    }

    //정답인지 아닌지 체크합니다.
    public bool CheckWin()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                int num = puzzleArr[i, j] - 1;

                if (puzzleList[num].num != i * 3 + j + 1)
                {
                    return false;
                }
            }
        }

        return true;
    }

    private int CheckPuzzle()
    {
        int cnt = 0;

        for (int i = 0; i < 9; i++)
            for (int j = i; j < 9; j++)
                if (randomNumbers[i] > randomNumbers[j])
                    cnt++;

        return cnt;
    }

    // 무질서도가 짝수가 될 때 까지 퍼즐을 새로 생성합니다.
    private void CorrectPuzzle()
    {
        int cnt = 1;

        while (cnt % 2 != 0)
        {
            MixPuzzle();
            cnt = CheckPuzzle();
            randomNumbers.Clear();
        }

        SetSprite();
    }
}
