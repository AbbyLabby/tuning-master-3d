using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField] private List<StageController> stagesControllers;
    
    public void ApplyStage(Stages.EnumStages stage)
    {
        foreach (var controller in stagesControllers.Where(controller => controller.stage == stage))
        {
            controller.StartStage();
            Debug.Log("Start Stage " + stage);
        }
    }
}
