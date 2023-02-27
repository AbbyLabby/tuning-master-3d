using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndRotate : MonoBehaviour
{
    public static DragAndRotate Singleton { get; private set; }

    private bool m_CanRotate = false;
    private bool m_NotImportantStage = true;

    private void Awake()
    {
        Singleton = this;
    }

    private void Update()
    {
        if(!m_NotImportantStage)
            return;
        
        if (Input.touchCount > 0)
        {
            var screenTouch = Input.GetTouch(0);
            
            if (EventSystem.current.IsPointerOverGameObject(screenTouch.fingerId))
                return;
            
            transform.Rotate(0f, screenTouch.deltaPosition.x * -.075f, 0f);
        }
    }

    public void EnableImportantStage()
    {
        m_NotImportantStage = false;
    }

    public void Rotate(float angle, float time)
    {
        if (m_NotImportantStage)
        {
            Debug.LogError("Firstly enable important stage! (DragAndRotate.Rotate())");
            return;
        }
        
        transform.DORotate(new Vector3(0, transform.eulerAngles.y + angle, 0), time);
    }

    public void SetRotation(float angle)
    {
        if (m_NotImportantStage)
        {
            Debug.LogError("Firstly enable important stage! (DragAndRotate.Rotate())");
            return;
        }

        transform.eulerAngles = new Vector3(0, angle, 0);
    }

    public void DisableImportantStage()
    {
        m_NotImportantStage = true;
    }
}