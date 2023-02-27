using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Dragger : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private StickerStage stickerStage;

    private Image image;

    private Vector2 m_StartPos;
    public Texture texture;

    private void Awake()
    {
        image = gameObject.GetComponent<Image>();
    }

    public void BeginDragHandler(BaseEventData data)
    {
        stickerStage.UpdateDecal(texture);
        
        stickerStage.DecalDragged();

        image.raycastTarget = false;
        
        canvasGroup.alpha = .25f;
        
        m_StartPos = transform.position;
    }

    public void DragHandler(BaseEventData data)
    {
        var pointerData = (PointerEventData)data;

        RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)canvas.transform, pointerData.position,
            canvas.worldCamera, out var pos);

        transform.position = new Vector3(canvas.transform.TransformPoint(pos).x,
            canvas.transform.TransformPoint(pos).y + 175, canvas.transform.TransformPoint(pos).z);
    }

    public void EndDragHandler(BaseEventData data)
    {
        canvasGroup.alpha = 1f;
        
        image.raycastTarget = true;

        transform.position = m_StartPos;
    }
}