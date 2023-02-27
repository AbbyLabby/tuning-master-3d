using PaintIn3D;
using UnityEngine;

public class StickerStage : MonoBehaviour, IStage
{
    private StageController m_StageController;

    [SerializeField] private P3dPaintDecal paintDecal;
    [SerializeField] private GameObject decalDoneButton;
    [SerializeField] private GameObject canvasUI;
    [SerializeField] private GameObject diskObj;

    private bool _isFirst = false;

    private bool _isPressed = false;

    private void Start()
    {
        EventsData.UpdateDecalEvent.AddListener(UpdateDecal);
    }

    public void StageStart(StageController stageController)
    {
        m_StageController = stageController;
        canvasUI.SetActive(true);

        paintDecal.gameObject.SetActive(true);
    }

    public void UpdateDecal(Texture texture)
    {
        paintDecal.Texture = texture;

        if (_isFirst)
            return;

        decalDoneButton.SetActive(true);
        _isFirst = true;
    }

    private void Update()
    {
        if (Input.touchCount <= 0)
            return;
        
        if(!_isPressed)
            return;

        var touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Ended)
        {
            var ray = Camera.main.ScreenPointToRay(new Vector3(touch.position.x, touch.position.y + 125));
            var hit = new RaycastHit();

            if (Physics.Raycast(ray, out hit))
            {
                var priority = 0;
                var pressure = 1.0f;
                var seed = 0;
                var rotation = Quaternion.LookRotation(-hit.normal);

                paintDecal.HandleHitPoint(false, priority, pressure, seed, hit.point, rotation);

                _isPressed = false;
                DragAndRotate.Singleton.DisableImportantStage();
            }
        }
    }

    public void DecalDragged()
    {
        _isPressed = true;
        
        DragAndRotate.Singleton.EnableImportantStage();
    }

    public void StageEnd()
    {
        canvasUI.SetActive(false);
        m_StageController.EndStage();
    }

    private void OnDisable()
    {
        EventsData.UpdateDecalEvent.RemoveAllListeners();
    }
}