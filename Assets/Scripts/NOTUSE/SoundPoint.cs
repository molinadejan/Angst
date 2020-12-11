/*
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[ExecuteInEditMode]
public class SoundPoint : MonoBehaviour
{
    public float checkDis;

    public SoundPoint parentPoint = null;
    public List<SoundPoint> nextPoints;

    public Color centerColor = Color.green;
    public Color lineColor;
    public Color areaColor;

    private Transform player;
    private float disToPlayer;

    [HideInInspector] public float disToOrigin = 0;
    [HideInInspector] public float disToOriginAndPlayer;

    private Vector3 target;

    private void Awake()
    {
        player = FindObjectOfType<PlayerControl>().cams.transform;

        if(Application.isPlaying && parentPoint != null)
        {
            disToOrigin = Vector3.Distance(transform.position, parentPoint.transform.position);
        }
    }

    private void Start()
    {
        if (Application.isPlaying)
        {
            SoundPoint cur = this;

            while (cur.parentPoint != null)
            {
                disToOrigin += cur.parentPoint.disToOrigin;
                cur = cur.parentPoint;
            }
        }
    }

    public void AddNewPoint()
    { 
        GameObject obj = new GameObject();
        obj.AddComponent<SoundPoint>();
        SoundPoint objSp = obj.GetComponent<SoundPoint>();

        obj.transform.SetParent(transform);
        objSp.parentPoint = this;
        obj.transform.localPosition = Vector3.zero;

        obj.transform.name = transform.name + "-" + nextPoints.Count;
        
        nextPoints.Add(objSp);
    }

    private void OnDestroy()
    {
        if (Time.frameCount == 0 || Application.isPlaying) return;

        parentPoint.nextPoints.Remove(this);
        parentPoint.nextPoints.TrimExcess();
    }

    public bool IsPlayerWithDis()
    {
        disToPlayer = Vector3.Distance(transform.position, player.position);
        return disToPlayer <= checkDis;
    }

    public bool IsObstacleBetweenPlayerAndPoint()
    {
        target = player.position - transform.position;

        bool ret = Physics.Raycast(transform.position, target.normalized, disToPlayer, 1 << 8);

        if (!ret)
        {
            disToOriginAndPlayer = disToOrigin + disToPlayer;
            Debug.DrawRay(transform.position, target, Color.yellow);
        }

        return ret;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = centerColor;
        Gizmos.DrawSphere(transform.position, 0.5f);

        if (nextPoints == null) return;

        for(int i = 0; i < nextPoints.Count; i++)
        {
            Gizmos.color = lineColor;
            Gizmos.DrawLine(transform.position, nextPoints[i].transform.position);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = areaColor;
        Gizmos.DrawSphere(transform.position, checkDis);
    }
}
*/