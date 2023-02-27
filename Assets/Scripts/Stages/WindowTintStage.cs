using UnityEngine;

public class WindowTintStage : MonoBehaviour, IStage
{
    private StageController m_StageController;

    [Range(0, 1)] [SerializeField] private float glassTransparent;

    private Color m_CurrentColor = new Color();
    private Color m_LastColor = new Color();

    private void Start()
    {
        EventsData.ChangeColorEvent.AddListener(UpdateColor);
        //EventsData.End.AddListener(StageEnd);
    }

    public void StageStart(StageController stageController)
    {
        m_StageController = stageController;
        EventsData.StartColorPickerEvent.Invoke();
    }

    private void Update()
    {
        if (m_LastColor == m_CurrentColor)
            return;

        UpdateGlassColor();
        m_LastColor = m_CurrentColor;
        //_currentColor = colorPicker.GetColor();
    }

    private void UpdateColor(Color color)
    {
        m_CurrentColor = color;

        UpdateGlassColor();
        
        StageEnd();
    }

    private void UpdateGlassColor()
    {
        foreach (var mesh in CarManager.Singleton.GetCar().glasses)
        {
            mesh.materials[0].color = new Color(m_CurrentColor.r, m_CurrentColor.g, m_CurrentColor.b, glassTransparent);
        }
    }

    public void StageEnd()
    {
        m_StageController.EndStage();
    }

    private void OnDisable()
    {
        EventsData.ChangeColorEvent.RemoveAllListeners();
    }
}