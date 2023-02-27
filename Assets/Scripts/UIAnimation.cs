using DG.Tweening;
using UnityEngine;

public class UIAnimation : MonoBehaviour
{
    [SerializeField] private Vector3 flowPos;
    [SerializeField] private Vector3 targetPos;
    [SerializeField] private float duration;

    private void OnEnable()
    {
        transform.localPosition = flowPos;

        transform.DOLocalMove(targetPos, duration);
    }

    private void OnDisable()
    {
        transform.DOLocalMove(flowPos, duration);
    }
}
