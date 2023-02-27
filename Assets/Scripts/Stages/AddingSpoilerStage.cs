using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class AddingSpoilerStage : MonoBehaviour, IStage
{
    private StageController m_StageController;

    private MeshFilter spoilerFilter;
    private GameObject spoilerParent;

    [SerializeField] private Slider spoilerSlider;
    [SerializeField] private float rotateTime;

    [SerializeField] private GameObject chooseSpoilerUI;
    [SerializeField] private GameObject addSpoilerUI;

    private Tween m_CurrentTween;
    private bool m_IsStarted = false;

    public void StageStart(StageController stageController)
    {
        m_StageController = stageController;
        chooseSpoilerUI.SetActive(true);

        spoilerFilter = CarManager.Singleton.GetCar().spoilerObj.GetComponent<MeshFilter>();
        spoilerParent = CarManager.Singleton.GetCar().spoilerParent;

        DragAndRotate.Singleton.EnableImportantStage();
    }

    private void OnEnable()
    {
        EventsData.ChooseSpoilerEvent.AddListener(UpdateSpoiler);
    }

    private void Update()
    {
        UpdateSlider();
    }

    private void UpdateSpoiler(Mesh spoilerMesh)
    {
        if (m_IsStarted)
            return;

        spoilerFilter.mesh = spoilerMesh;
        chooseSpoilerUI.SetActive(false);
        addSpoilerUI.SetActive(true);
        spoilerParent.SetActive(true);

        spoilerSlider.maxValue = 15;
        spoilerSlider.minValue = -15;
        spoilerSlider.value = 0;

        StartSpoilerFitting();
    }

    private void UpdateSlider()
    {
        if (spoilerFilter.transform.localEulerAngles.y > 15 || spoilerFilter.transform.localEulerAngles.y < 0)
        {
            spoilerSlider.value =spoilerFilter.transform.localEulerAngles.y - 360;
        }
        else
        {
            spoilerSlider.value = spoilerFilter.transform.localEulerAngles.y;
        }
    }

    public void StopButton()
    {
        m_CurrentTween.Kill();
        CarManager.Singleton.GetCar().rigidbody.AddRelativeForce(Vector3.down * 300);
        spoilerFilter.transform.localPosition = Vector3.zero;

        DragAndRotate.Singleton.DisableImportantStage();

        StartCoroutine(OffDelay());
    }

    private void StartSpoilerFitting()
    {
        spoilerFilter.transform.localPosition = new Vector3(
            spoilerFilter.transform.localPosition.x,
            spoilerFilter.transform.localPosition.y + 0.3f,
            spoilerFilter.transform.localPosition.z - 0.3f);

        StartCoroutine(StartDelay());

        m_IsStarted = true;
        CarManager.Singleton.GetCar().vSpoilerCam.SetActive(true);
    }

    private void RotateLeft()
    {
        m_CurrentTween = spoilerFilter.transform
            .DOLocalRotate(new Vector3(0, -30, 0), rotateTime, RotateMode.LocalAxisAdd)
            .OnComplete(RotateRight);
    }

    private void RotateRight()
    {
        m_CurrentTween = spoilerFilter.transform
            .DOLocalRotate(new Vector3(0, 30, 0), rotateTime, RotateMode.LocalAxisAdd)
            .OnComplete(RotateLeft);
    }

    private IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(1);
        m_CurrentTween = spoilerFilter.transform
            .DOLocalRotate(new Vector3(0, -15, 0), rotateTime, RotateMode.LocalAxisAdd)
            .OnComplete(RotateRight);
    }

    private IEnumerator OffDelay()
    {
        yield return new WaitForSeconds(1);
        StageEnd();
    }

    public void StageEnd()
    {
        addSpoilerUI.SetActive(false);
        CarManager.Singleton.GetCar().vSpoilerCam.SetActive(false);
        m_StageController.EndStage();
    }

    private void OnDisable()
    {
        EventsData.ChooseSpoilerEvent.RemoveAllListeners();
    }
}