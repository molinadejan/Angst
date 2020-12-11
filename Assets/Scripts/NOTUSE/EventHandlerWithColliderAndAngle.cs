/*
using UnityEngine;

public class EventHandlerWithColliderAndAngle : EventHandlerBase
{
    public Transform EnemySpotPos;
    public float triggerAngle;

    private bool isEnter;
    private Vector3 target;
    private BoxCollider box;

    public float angle;

    private void Awake()
    {
        box = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            isEnter = true;
            box.enabled = false;
        }
    }

    private void Update()
    {
        if(isEnter)
        {
            target = EnemySpotPos.position - PlayerControl.instance.cams.transform.position;
            target = target.normalized;

            float dot = Vector3.Dot(PlayerControl.instance.cams.transform.forward, target);
            angle = Mathf.Acos(dot) * Mathf.Rad2Deg;

            if (angle <= triggerAngle)
                InvokeEvent();
        }
    }
}
*/
