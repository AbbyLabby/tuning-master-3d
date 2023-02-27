using PaintIn3D;
using UnityEngine;

public class PolishingStage : MonoBehaviour, IStage
{
    [SerializeField] private CarBufferScript carBufferScript;
    [SerializeField] private P3dPaintSphere paintSphere;
    [SerializeField] private GameObject doneButton;
    [SerializeField] private GameObject canvasUI;
    [SerializeField] private P3dChangeCounterText text;
    [SerializeField] private GameObject diskObj;
    [SerializeField] private P3dPaintFill paintFill;

    private StageController m_StageController;
    private RaycastHit lastHit;

    public void StageStart(StageController stageController)
    {
        m_StageController = stageController;
        canvasUI.SetActive(true);
        DragAndRotate.Singleton.EnableImportantStage();

        CarManager.Singleton.GetCar().changeCounterRust.SetActive(true);
    }

    private void Update()
    {
        if (Input.touchCount <= 0)
            return;

        var touch = Input.GetTouch(0);

        var ray = Camera.main.ScreenPointToRay(new Vector3(touch.position.x, touch.position.y + 125));
        var hit = new RaycastHit();

        if (Physics.Raycast(ray, out hit))
        {
            var
                priority = 0; // If you're painting multiple times per frame, or using 'live painting', then this can be used to sort the paint draw order. This should normally be set to 0.
            var
                pressure = 1.0f; // If you're using modifiers that use paint pressure (e.g. from a finger), then you can set it here. This should normally be set to 1.
            var
                seed = 0; // If this paint uses modifiers that aren't marked as 'Unique', then this seed will be used. This should normally be set to 0.
            var rotation =
                Quaternion.LookRotation(-hit
                    .normal); // Get the rotation of the paint. This should point TOWARD the surface we want to paint, so we use the inverse normal.

            paintSphere.HandleHitPoint(false, priority, pressure, seed, hit.point, rotation);

            carBufferScript.MoveCarBuffer(hit.point);
            carBufferScript.RotateCarBuffer(hit.normal);

            CalculateTouchMoving(touch);

            if (hit.collider.CompareTag("Body"))
            {
                lastHit = hit;
            }
        }
        
        switch (touch.phase)
        {
            case TouchPhase.Began:
                carBufferScript.StartBufferAnim();
                break;
            case TouchPhase.Ended:
                carBufferScript.StopBufferAnim();
                break;
        }


        if (doneButton.activeInHierarchy)
            return;

        if (text.percent >= 70)
            EnableDoneButton();
    }

    private void EnableDoneButton()
    {
        doneButton.SetActive(true);
    }

    public void StageEnd()
    {
        m_StageController.EndStage();
        
        paintSphere.gameObject.SetActive(false);
        paintFill.gameObject.SetActive(true);
        
        var rotation = Quaternion.LookRotation(-lastHit.normal);
        
        paintFill.HandleHitCoord(false, 0, 1, 1, new P3dHit(lastHit), rotation);
        
        CarManager.Singleton.GetCar().changeCounterRust.SetActive(false);
        CarManager.Singleton.GetCar().bodyRenderer.sharedMaterial.SetFloat("_GlossMapScale", 1);
        canvasUI.SetActive(false);
    }

    private void CalculateTouchMoving(Touch touch)
    {
        var screenCenter = Screen.width / 2f;
        var bias = (touch.position.x - screenCenter).Remap(-screenCenter, screenCenter, -100, 100);

        var r = diskObj.transform.rotation;
        if (bias < -4.5f)
            diskObj.transform.rotation =
                Quaternion.Euler(r.eulerAngles.x, r.eulerAngles.y + bias * Time.deltaTime, r.eulerAngles.z);
        if (bias > 4.5f)
            diskObj.transform.rotation =
                Quaternion.Euler(r.eulerAngles.x, r.eulerAngles.y + bias * Time.deltaTime, r.eulerAngles.z);
    }
}