/*
using UnityEngine;
using DG.Tweening;

public class ObjectGravity : MonoBehaviour
{
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        GravityChangeManager.GravityChange_Event += new GravityChange_Dele(ObjectsGravityChange);
        GravityChangeManager.GravityChangeFinish_Event += new GravityChange_Dele(ObjectsGravityChangeFinish);
    }

    private void ObjectsGravityChange()
    {
        Vector3 target = transform.position;

        RaycastHit hit;

        rb.useGravity = false;

        if (Physics.Raycast(transform.position, Vector3.up, out hit, 1000f, 8))
        {
            if (hit.distance > 6)
                target += Vector3.up * Random.Range(0.5f, 3f);
            else
                target += Vector3.up * (hit.distance / 2);
        }

        rb.AddTorque(Random.insideUnitSphere * 5);
        transform.DOMove(target, 2f);
    }

    private void ObjectsGravityChangeFinish()
    {
        rb.useGravity = true;
    }
}
*/
