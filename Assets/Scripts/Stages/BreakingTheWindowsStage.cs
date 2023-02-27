using System.Collections;
using Cinemachine;
using PaintIn3D;
using UnityEngine;

public class BreakingTheWindowsStage : MonoBehaviour, IStage
{
    [SerializeField] private GameObject canvasUI;
    [SerializeField] private float rotatingSpeed;
    [SerializeField] private Transform disk;
    [SerializeField] private Material glassMaterial;
    [SerializeField] private P3dPaintDecal paintDecal;
    [SerializeField] private HammerScript hammerScript;
    [SerializeField] private CinemachineVirtualCamera vCam;
    
    [SerializeField] private Vector2 scaleRange;

    [SerializeField] private float shakeTime;
    [SerializeField] private float shakeAmplitude;

    private StageController m_StageController;
    private int glassCount;

    private void OnEnable()
    {
        EventsData.GlassBrokenEvent.AddListener(GlassBroken);
    }

    public void StageStart(StageController stageController)
    {
        m_StageController = stageController;

        DragAndRotate.Singleton.EnableImportantStage();

        CarManager.Singleton.GetCar().bodyRenderer.GetComponent<MeshCollider>().enabled = false;

        CalculateGlassCount();

        canvasUI.SetActive(true);
    }

    private void CalculateGlassCount()
    {
        var glasses = CarManager.Singleton.GetCar().glasses;
        
        foreach (var glass in glasses)
        {
            var component = glass.GetComponent<GlassScript>();

            if (component != null)
            {
                glassCount++;
            }
        }
    }

    private void Update()
    {
        disk.eulerAngles = new Vector3(disk.eulerAngles.x,
            disk.eulerAngles.y + rotatingSpeed * Time.deltaTime, disk.eulerAngles.z);

        if (Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                var ray = Camera.main.ScreenPointToRay(new Vector2(touch.position.x, touch.position.y));
                var hit = new RaycastHit();

                if (Physics.Raycast(ray, out hit))
                {
                    hammerScript.Kick(hit);

                    if (hit.collider.CompareTag("Glass"))
                    {
                        StartCoroutine(DelayPaint(hit));
                    }
                }
            }
        }
    }

    private IEnumerator ShakeCamera(float time, float amplitude)
    {
        var noise = vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        
        noise.m_FrequencyGain = 1;
        noise.m_AmplitudeGain = amplitude;

        yield return new WaitForSeconds(time);
        
        noise.m_FrequencyGain = 0;
        noise.m_AmplitudeGain = 0;
    }

    private IEnumerator DelayPaint(RaycastHit hit)
    {
        yield return new WaitForSeconds(.2f);
        
        StartCoroutine(ShakeCamera(shakeTime, shakeAmplitude));
        
        hit.collider.GetComponent<IGlass>().Hit(hit);

        var rotation = Quaternion.LookRotation(-hit.normal);

        paintDecal.Radius = Random.Range(scaleRange.x, scaleRange.y);

        paintDecal.HandleHitPoint(false, 0, 1, 1, hit.point, rotation);
    }

    private void GlassBroken()
    {
        glassCount--;

        if (glassCount == 0)
            StageEnd();
    }

    private void ReplaceGlassMaterial()
    {
        var color = new Color(0, 0, 0, 0);
        var glasses = CarManager.Singleton.GetCar().glasses;

        foreach (var glass in glasses)
        {
            glass.material = glassMaterial;
            glass.material.color = color;
        }
    }

    public void StageEnd()
    {
        canvasUI.SetActive(false);
        DragAndRotate.Singleton.DisableImportantStage();
        CarManager.Singleton.GetCar().bodyRenderer.GetComponent<MeshCollider>().enabled = true;
        ReplaceGlassMaterial();

        m_StageController.EndStage();
    }

    private void OnDisable()
    {
        EventsData.GlassBrokenEvent.RemoveAllListeners();
    }
}