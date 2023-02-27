using UnityEngine;

public class StageController : MonoBehaviour
{
    public Stages.EnumStages stage;

    [SerializeField] private GameObject worldObj;
    
    private IStage stageScript;

    private void Awake()
    {
        stageScript = worldObj.GetComponent<IStage>();
    }

    public void StartStage()
    {
        if (worldObj != null)
            worldObj.SetActive(true);

        stageScript.StageStart(this);
    }

    public void EndStage()
    {
        if (worldObj != null)
            worldObj.SetActive(false);

        LevelManager.Singleton.NextStage();
    }
}