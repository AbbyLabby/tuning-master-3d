using UnityEngine;
using UnityEngine.SceneManagement;

public class EndStage : MonoBehaviour, IStage
{
    [SerializeField] private GameObject canvasUI;
    public void StageStart(StageController stageController)
    {
        canvasUI.SetActive(true);
    }

    public void StageEnd()
    {
        SceneManager.LoadScene(0);
    }
}
