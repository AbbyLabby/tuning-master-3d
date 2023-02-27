using DG.Tweening;
using UnityEngine;

public class ClampDragScript : MonoBehaviour
{
    [SerializeField] private Vector3 targetPos;
    [SerializeField] private Vector3 targetRotation;
    [SerializeField] private GameObject particle;

    private bool isConnected = false;

    public void OnMouseDrag()
    {
        if(isConnected)
            return;
        
        var touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Moved)
        {
            transform.localPosition = new Vector3(transform.localPosition.x - touch.deltaPosition.x * .0005f,
                transform.localPosition.y + touch.deltaPosition.y * .0005f, transform.localPosition.z);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ChargerBase"))
        {
            isConnected = true;
            
            SetTargetPos();
            
            particle.SetActive(true);

            EventsData.CableConnectedEvent.Invoke();
        }
    }

    private void SetTargetPos()
    {
        transform.DOLocalMove(targetPos, .5f);
        transform.DOLocalRotate(targetRotation, .5f);
    }
}