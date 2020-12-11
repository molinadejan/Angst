/*
using System.Collections.Generic;
using UnityEngine;

public class SoundControl : MonoBehaviour
{
    public SoundPoint startPoint;
    public AudioSource audioSource;

    private Queue<SoundPoint> pointList = new Queue<SoundPoint>();

    private float volume;
    private float maxDis;
    private float totalDis;

    private void Awake()
    {
        maxDis = audioSource.maxDistance;
        volume = audioSource.volume;
        audioSource.volume = 0;
    }

    private void Update()
    {
        SoundOnOff(BFSToFindNearestPoint());
    }

    private bool BFSToFindNearestPoint()
    {
        bool ret = false;

        pointList.Enqueue(startPoint);

        while(pointList.Count != 0)
        {
            SoundPoint curPoint = pointList.Dequeue();

            if(curPoint.IsPlayerWithDis())
            {
                if(curPoint.IsObstacleBetweenPlayerAndPoint())
                {
                    for (int i = 0; i < curPoint.nextPoints.Count; ++i)
                        pointList.Enqueue(curPoint.nextPoints[i]);
                }
                else
                {
                    totalDis = curPoint.disToOriginAndPlayer;
                    ret = true;
                    break;
                }
            }
        }

        pointList.Clear();
        pointList.TrimExcess();

        return ret;
    }

    public void SoundOnOff(bool Check)
    {
        if (totalDis >= maxDis || !Check)
            audioSource.volume = 0;
        else
            audioSource.volume = volume * (1 - totalDis / maxDis);
    }
}
*/