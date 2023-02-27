using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonsScale : MonoBehaviour
{
    private Vector3 _startScale;

    private void Awake()
    {
        _startScale = transform.localScale;
    }

    public void PointerEnterHandler(BaseEventData data)
    {
        transform.DOScale(new Vector3(transform.localScale.x * .85f, transform.localScale.y * .85f), .5f);
    }

    public void PointerExitHandler(BaseEventData data)
    {
        transform.DOScale(new Vector3(_startScale.x, _startScale.y), .5f);
    }
}
