using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PowerPluginStage : MonoBehaviour, IStage
{
    [SerializeField] private List<MeshRenderer> firstStepLights;
    [SerializeField] private List<MeshRenderer> secondStepLights;
    [SerializeField] private Transform charger;
    [SerializeField] private Vector3 targetPosition;
    [SerializeField] private Vector3 targetRotation;

    [SerializeField] private List<GameObject> particles;
    [SerializeField] private List<Material> lightMaterials;

    private StageController m_StageController;
    private int count = 0;

    private bool redCable = false;
    private bool blueCable = false;

    private void OnEnable()
    {
        EventsData.CableConnectedEvent.AddListener(OnCableConnected);
    }

    public void StageStart(StageController stageController)
    {
        m_StageController = stageController;
        DragAndRotate.Singleton.EnableImportantStage();
        DragAndRotate.Singleton.SetRotation(0);
    }

    private void OnCableConnected()
    {
        count++;

        switch (count)
        {
            case 1:
                StartCoroutine(EnableFirstStep());
                break;
            case 2:
                StartCoroutine(EnableSecondStep());
                MoveChargerBase();
                break;
        }
    }

    private IEnumerator EnableFirstStep()
    {
        foreach (var light in firstStepLights)
        {
            light.material.EnableKeyword("_EMISSION");
            yield return new WaitForSeconds(.1f);
        }
    }

    private IEnumerator EnableSecondStep()
    {
        foreach (var light in secondStepLights)
        {
            light.material.EnableKeyword("_EMISSION");
            yield return new WaitForSeconds(.1f);
        }
    }

    public void CheckButtonPress()
    {
        if (count == 2)
        {
            foreach (var particle in particles)
            {
                particle.SetActive(true);
                EnableExhaust();
                EnableLights();
                StartEngine();
                
                charger.gameObject.SetActive(false);
                DragAndRotate.Singleton.DisableImportantStage();
            }
        }
    }

    private void EnableExhaust()
    {
        CarManager.Singleton.GetCar().exhaustParticle.SetActive(true);
    }

    private void EnableLights()
    {
        foreach (var mat in lightMaterials)
        {
            mat.EnableKeyword("_EMISSION");
        }
    }

    private void StartEngine()
    {
        var body = CarManager.Singleton.GetCar().body.transform;

        body.DOShakeRotation(1.5f, new Vector3(0, 0, 1.5f)).OnComplete(StartEnd);
    }   

    private void StartEnd()
    {
        StartCoroutine(EndDelay());
    }
    
    private IEnumerator EndDelay()
    {
        yield return new WaitForSeconds(3f);
        StageEnd();
    }

    private void MoveChargerBase()
    {
        charger.DOLocalMove(targetPosition, 1f).SetDelay(.5f);
        charger.DOLocalRotate(targetRotation, 1f).SetDelay(.5f);
    }

    public void StageEnd()
    {
        m_StageController.EndStage();
    }

    private void OnDisable()
    {
        EventsData.CableConnectedEvent.RemoveAllListeners();
    }
}
