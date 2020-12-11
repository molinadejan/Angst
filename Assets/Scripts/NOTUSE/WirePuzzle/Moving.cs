/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Moving : MonoBehaviour
{
    public GameObject sparkPrefab;
    private GameObject spark;
    public Camera UICamera;
    public MapArr[] puzzle;

    public int[,] pMap;
    bool[,] check;

    int[] dy = new int[4] { 0, -1, 0, 1 };
    int[] dx = new int[4] { -1, 0, 1, 0 };
    //좌 상 우 하 
    
    public int MaxY = 5;
    public int MaxX = 5;
    public int MinY = 1;
    public int MinX = 1;
    public int WireNumber=0;
    private bool CompletePuzzle = false;
    //min 값은 고정 max값만 변경가능. max 값 +1 만큼 오브젝트 생성필요.
    int destinationy;
    int destinationx;

    struct positon
    {
        public int x;
        public int y;
    };
    positon Myposition;
    Stack<positon> positions;

    public float tt=0f;
    public float dt = 1000f;
    int cnt=0;
    bool cango(int y, int x)
    {
        for (int i = 0; i < 4; i++)
        {

            if (y + dy[i] >= MinY && y + dy[i] < MaxY - 1 && x + dx[i] >= MinX && x + dx[i] < MaxX - 1)
            {

                if (pMap[y + dy[i], x + dx[i]] == 0)
                {
                    return false;
                }
            }
        }
        return true;
    }//갈수있는곳인지

    bool isRange(int y, int x)//범위체크
    {
        if (x >= MinX && x < MaxX && y >= MinY && y < MaxY)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        positions = new Stack<positon>();
        pMap = new int[MaxY+1, MaxX+1];
        check = new bool[MaxY + 1, MaxX + 1];
        //겉 태두리 추가 -> 시작 끝지점 추가를 위해서. 
        Myposition.x = 0;
        Myposition.y = 1;
        positions.Push(Myposition);
        puzzle[1].Map[0].StartEndImage(5);

        pMap[1, 1] = 1;
        

        Myposition.x = 1;
        Myposition.y = 1;
        positions.Push(Myposition);


        findPath();
        setImage();
        SetOtherImage();

        CreateSpark();
    }

    // Update is called once per frame
    void Update()
    {
        Myposition.y = 1;
        Myposition.x = 0;
        if (bfs(Myposition))
        {
            Debug.Log("clear");
            CompletePuzzle = true;
        }
        if (tt >= dt)
        {
            Destroy(spark);
            CreateSpark();
            tt = 0;
        }
        tt += Time.unscaledDeltaTime;
    }

    
    void CreateSpark()
    {
        float randomX = Random.Range(-35, 35);
        float randomY = Random.Range(-50, 50);
        Vector3 MousePosition = transform.transform.position;
        MousePosition = MousePosition + new Vector3(randomX, randomY,-5f);         
        spark = Instantiate(sparkPrefab, MousePosition,Quaternion.identity);
    }

    void findPath()//랜덤경로찾기
    {
        bool isend = false;

        int cnt = 0;

        while (!isend) {

            int mx = positions.Peek().x;
            int my = positions.Peek().y;
            int rand;

           

            if (my == MaxY - 1 || my == MinY || mx == MaxX - 1 || mx == MinX)
            {
                if (cango(my, mx) || cnt >= (MaxX + MaxY - 2))
                {
                    pMap[my, mx] = 1;
                    //Debug.Log(my);
                    //Debug.Log(mx);
                    if (my == MaxY - 1)
                    {
                        Myposition.x = mx;
                        Myposition.y = my + 1;
                    }
                    else if (my == MinY)
                    {
                        Myposition.x = mx;
                        Myposition.y = my - 1;
                    }
                    else if (mx == MaxX - 1)
                    {
                        Myposition.x = mx + 1;
                        Myposition.y = my;
                    }
                    else if (mx == MinX)
                    {
                        Myposition.x = mx - 1;
                        Myposition.y = my;
                    }
                    positions.Push(Myposition);
                    isend = true;
                    break;
                }
            }

            rand = Random.Range(0, 4);
            mx += dx[rand];
            my += dy[rand];

            if (isRange(my,mx) && pMap[my,mx]==0)
            {
                pMap[my, mx] = 1;
                Myposition.x = mx;
                Myposition.y = my;
                positions.Push(Myposition);
                cnt++;
                //Debug.Log(my);
                //Debug.Log(mx);
            }

            

        }
    }
    void setImage()//경로상의 이미지 stack에서 빼며 직선인지 곡선인지 체크
    {
        int x = positions.Peek().x;
        int y = positions.Peek().y;
        positions.Pop();

        destinationy = y;
        destinationx = x;
        if (destinationy == MinY - 1 || destinationy == MaxY)
        {
            puzzle[destinationy].Map[destinationx].StartEndImage(15);
        }else if(destinationx==MinX-1 || destinationx == MaxX)
        {
            puzzle[destinationy].Map[destinationx].StartEndImage(5);
        }
        Debug.Log(destinationy);
        Debug.Log(destinationx);
        int currentx = positions.Peek().x;
        int currenty = positions.Peek().y;

        positions.Pop();

        int nextx = positions.Peek().x;
        int nexty = positions.Peek().y;

        int gapx = makeAbsoluteNum(nextx - x);
        int gapy = makeAbsoluteNum(nexty - y);
        
        print(y);
        print(x);

        print(currenty);
        print(currentx);

        print(nexty);
        print(nextx);
        
        if (gapx == 1 && gapy == 1)
        {
            puzzle[currenty].Map[currentx].SetImageState(2);
            //curvy
        }
        else
        {
            puzzle[currenty].Map[currentx].SetImageState(1);
            //straight
        }
        while (true)
        {
            x = currentx;
            y = currenty;

            currentx = positions.Peek().x;
            currenty = positions.Peek().y;

            positions.Pop();

            nextx = positions.Peek().x;
            nexty = positions.Peek().y;

            gapx = makeAbsoluteNum(nextx - x);
            gapy = makeAbsoluteNum(nexty - y);


            if (gapx == 1 && gapy == 1)
            {
                puzzle[currenty].Map[currentx].SetImageState(2);
                //curvy
            }
            else
            {
                puzzle[currenty].Map[currentx].SetImageState(1);
                //straight
            }
            if (nextx == 0 && nexty == 1)
            {
                break;
            }

        }

    }

    void SetOtherImage()//경로가 아닌 위치 랜덤 이미지 세팅
    {
        for(int i = MinY; i < MaxY; i++)
        {
            for(int j = MinX; j < MaxX; j++)
            {
                if (pMap[i, j] != 1)//방문 안했으면
                {
                    int rndShape = Random.Range(1,4);
                    puzzle[i].Map[j].SetImageState(rndShape);
                }
            }
        }
    }

   

    int makeAbsoluteNum(int num)
    {
        if (num > 0)
        {
            return num;
        }
        else
        {
            return -num;
        }
    }//항상 양수로 만들어줌

    bool bfs(positon p)//정답 확인을 위한 bfs
    {
        for(int i = 0; i < MaxY; i++)
        {
            for(int j = 0; j < MaxX; j++)
            {
                check[i, j] = false;
            }
        }//초기화 
        Queue<positon> q = new Queue<positon>();
        q.Enqueue(p);
        check[p.y, p.x] = true;

        while (q.Count > 0)
        {
            int y = q.Peek().y;
            int x = q.Peek().x;

            q.Dequeue();
            for(int i = 0; i < 4; i++)
            {
                positon temp;//저장을 위한것.
                temp.y = y + dy[i];
                temp.x = x + dx[i];
                int tempy = y + dy[i];
                int tempx = x + dx[i]; 
                if ( (tempx >= MinX-1 && tempx < MaxX+1 && tempy >= MinY-1 && tempy < MaxY+1) && !check[tempy, tempx])//범위체크를 다르게 해야됨
                {
                    int Canigo = puzzle[y].Map[x].GetLineState();
                    int Canyougo = puzzle[tempy].Map[tempx].GetLineState();
                    if((Canigo &  (1<<i)) == (1<<i))//내가 갈 수 있는 방향인지 판별.
                    { 
                      if((Canyougo & (1 << ((i + 2) % 4))) == (1 << (i + 2) % 4))//상대방쪽이 올 수 있는 방향인지 판별. 항상 서로의 반대 180도를 검사 
                      {
                            temp.y = tempy;
                            temp.x = tempx;
                            q.Enqueue(temp);
                            check[tempy, tempx] = true;
                      }  
                    }
                   
                }
            }
        }

        if (check[destinationy, destinationx])//마지막까지 가게 된다면.
        {
            Debug.Log("end");
            return true;
        }
        else
        {
            return false;
        }
        
    }

    public bool ComPlete
    {
        get
        {
            return CompletePuzzle;
        }
        set
        {
            CompletePuzzle = value;
        }
    }
    public int PuzzleNumber
    {
        get
        {
            return WireNumber;
        }
        set {
            WireNumber = value;
        }
    }
 
}
*/
