/*
#pragma warning disable CS0649

using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCross : MonoBehaviour
{
    public static PlayerCross instance = null;

    public Text crossCountText;
    public Image crosscoolTimeImage;

    public float crossRange;

    [SerializeField] private float crossDelay;

    private bool isCrossDelay;
    private float crossDelayCount;
    private int crossCount;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    private void Update()
    {
        if (InputManager.instance.crossUse && !UIManager.instance.GetMenuState() && !PlayerControl.instance.isMoveLock)
        {
            UseCross();
        }
    }

    public void GetCross(int n)
    {
        crossCount += n;
        UIManager.instance.SetCountText(crossCountText, crossCount);
    }

    public void UseCross()
    {
        if (crossCount <= 0 || isCrossDelay || !MotherEnemy.instance.gameObject.activeSelf)
        {
            return;
        }

        if(Vector3.Distance(transform.position, MotherEnemy.instance.transform.position) > crossRange)
        {
            print("Out Of Range");
            return;
        }

        MotherEnemy.instance.SetEnemyState(EnemyState.Stun);

        print("Use Cross");
        --crossCount;

        UIManager.instance.SetCountText(crossCountText, crossCount);
        UIManager.instance.SetCountImage(crosscoolTimeImage, crossDelay);

        //UIManager.instance.PlayUISound();

        DOTween.To(() => crossDelayCount, x => crossDelayCount = x, crossDelay, crossDelay)
            .SetEase(Ease.Linear)
            .OnStart(() =>
            {
                isCrossDelay = true;
            })
            .OnComplete(() =>
            {
                isCrossDelay = false;
                crossDelayCount = 0;
            });
    }
}
*/
