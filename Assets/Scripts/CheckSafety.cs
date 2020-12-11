using UnityEngine;

public class CheckSafety : MonoBehaviour
{
    public static CheckSafety instance = null;

    public bool isInSafety = true;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
            isInSafety = true;
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
            isInSafety = false;
    }
}
