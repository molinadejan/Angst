using UnityEngine;

public class SlotButton : MonoBehaviour
{
    public LockManager lockManager;

    private float rotation;
    [HideInInspector] public bool moved = false;

    private int myState = 0;

    public int State
    {
        get { return myState; }
        set 
        { 
            myState = value % lockManager.numberCount;
        }
    }

    private void Update()
    {
        float rotate = lockManager.velocity * Time.deltaTime;

        if (rotation > rotate)
        {
            rotation -= rotate;
        }
        else
        {
            rotate = rotation;
            rotation = 0;
            moved = false;
        }
        
        transform.Rotate(-rotate, 0, 0);
    }

    public void OnMouseDown()
    {
        if (moved) return;

        lockManager.audioSource.Play();
        rotation = lockManager.rotateAngle;
        ++State;
        moved = true;
    }
}
