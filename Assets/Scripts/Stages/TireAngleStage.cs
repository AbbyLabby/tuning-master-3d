using UnityEngine;

public class TireAngleStage : MonoBehaviour, IStage
{
    private StageController m_StageController;

    [SerializeField] private GameObject canvasUI;

    private float m_CurrentAngle = 0;
    private float m_LastAngle = 0;

    private float m_Angle1 = 0;
    private float m_Angle2 = 0;

    public void StageStart(StageController stageController)
    {
        m_StageController = stageController;
        canvasUI.SetActive(true);
    }

    public void StageEnd()
    {
        canvasUI.SetActive(false);
        m_StageController.EndStage();
    }

    private void Update()
    {
        if (m_CurrentAngle == m_LastAngle)
            return;

        CalculateAngles(m_CurrentAngle);
        UpdateTireAngles(m_Angle1, m_Angle2);

        m_LastAngle = m_CurrentAngle;
    }

    public void ChangeTireAngle(float angle)
    {
        m_CurrentAngle = angle;
    }

    private void UpdateTireAngles(float angle1, float angle2)
    {
        var currentAngle = 0f;
        var mult = -1;

        foreach (var tire in CarManager.Singleton.GetCar().tires)
        {
            if (mult < 0)
                currentAngle = angle1;
            else
                currentAngle = angle2;

            var eulerAngles = tire.transform.eulerAngles;

            eulerAngles =
                new Vector3(currentAngle,
                    eulerAngles.y, eulerAngles.z);

            tire.transform.eulerAngles = eulerAngles;

            mult *= -1;
        }
    }

    private void CalculateAngles(float angle)
    {
        m_Angle1 = angle;
        m_Angle2 = angle;
    }
}