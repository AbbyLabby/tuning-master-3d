using PaintIn3D;
using UnityEngine;

public class SprayPaintingStage : MonoBehaviour, IStage
{
    private StageController m_StageController;

    [SerializeField] private P3dPaintSphere paintSphere;
    [SerializeField] private DeodorantScript deodorantScript;
    [SerializeField] private GameObject doneButton;
    [SerializeField] private GameObject canvasUI;
    [SerializeField] private P3dChangeCounterText text;

    private bool m_ColorChoose = false;
    private Color m_Color = new Color();

    private float m_Time;

    private void Start()
    {
        m_ColorChoose = false;
    }

    private void OnEnable()
    {
        EventsData.ChangeColorEvent.AddListener(UpdateColor);
    }

    private void UpdateColor(Color color)
    {
        m_Color = color;

        deodorantScript.ChangeColor(m_Color);
        paintSphere.Color = m_Color;

        m_ColorChoose = true;

        EnablePaint();
    }

    public void ChangeColorButton()
    {
        DisablePaint();

        EventsData.StartColorPickerEvent.Invoke();
    }

    private void EnablePaint()
    {
        paintSphere.gameObject.SetActive(true);
        deodorantScript.gameObject.SetActive(true);
        canvasUI.SetActive(true);
    }

    private void DisablePaint()
    {
        paintSphere.gameObject.SetActive(false);
        deodorantScript.gameObject.SetActive(false);
        canvasUI.SetActive(false);
    }

    public void StageStart(StageController stageController)
    {
        m_StageController = stageController;
        EventsData.StartColorPickerEvent.Invoke();
        
        DragAndRotate.Singleton.EnableImportantStage();
        
        CarManager.Singleton.GetCar().changeCounter.SetActive(true);

        DisablePaint();
    }

    public void StageEnd()
    {
        m_StageController.EndStage();
        DisablePaint();
        
        DragAndRotate.Singleton.DisableImportantStage();

        CarManager.Singleton.GetCar().changeCounter.SetActive(false);
    }

    private void Update()
    {
        if (!m_ColorChoose)
            return;

        if (doneButton.activeInHierarchy)
            return;
        
        if(text.percent >= 50)
            EnableDoneButton();
    }

    private void EnableDoneButton()
    {
        doneButton.SetActive(true);
    }

    private void OnDisable()
    {
        EventsData.ChangeColorEvent.RemoveAllListeners();
    }
}