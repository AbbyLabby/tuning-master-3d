using UnityEngine;

public class ChooseTireStage : MonoBehaviour, IStage
{
    [SerializeField] private GameObject canvasUI;

    private StageController m_StageController;

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

    private void OnEnable()
    {
        EventsData.ChooseTireEvent.AddListener(UpdateTires);
    }

    private void UpdateTires(Material newMaterial)
    {
        var tires = CarManager.Singleton.GetCar().tires;

        foreach (var tire in tires)
        {
            tire.GetComponent<SkinnedMeshRenderer>().material = newMaterial;
        }
        
        StageEnd();
    }

    private void OnDisable()
    {
        EventsData.ChooseTireEvent.RemoveAllListeners();
    }
}