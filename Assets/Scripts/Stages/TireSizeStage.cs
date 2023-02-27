using System.Collections.Generic;
using UnityEngine;

public class TireSizeStage : MonoBehaviour, IStage
{
    [SerializeField] private float jumpPower = 20;
    [SerializeField] private GameObject canvasUI;

    private StageController m_StageController;

    private float percentage = 0;
    private float lastPercentage = 0;
    private Rigidbody rigidbody;

    public void StageStart(StageController stageController)
    {
        m_StageController = stageController;

        canvasUI.SetActive(true);
        rigidbody = CarManager.Singleton.GetCar().rigidbody;
    }

    public void StageEnd()
    {
        canvasUI.SetActive(false);
        m_StageController.EndStage();
    }

    private void Update()
    {
        if (percentage == lastPercentage)
            return;

        JumpSuspension(jumpPower, rigidbody);
        UpdateConnectedAnchor();
        UpdateTiresScale();
        UpdateTiresPosition(CarManager.Singleton.GetCar().tires);

        lastPercentage = percentage;
    }

    private void JumpSuspension(float power, Rigidbody _rigidbody)
    {
        _rigidbody.AddForce(CarManager.Singleton.GetCar().body.transform.up * power);
    }

    private void UpdateConnectedAnchor()
    {
        var newAnchor = Extensions.CalculatePercentage(.15f, .5f, percentage);

        CarManager.Singleton.GetCar().springJoint.connectedAnchor =
            new Vector3(0, newAnchor, 0);
    }

    private void UpdateTiresScale()
    {
        var newSize = Extensions.CalculatePercentage(.9f, 1.5f, percentage);

        foreach (var tire in CarManager.Singleton.GetCar().tires)
        {
            tire.transform.localScale = new Vector3(newSize, newSize, newSize);

            tire.GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0, percentage);
        }
    }

    private void UpdateTiresPosition(List<GameObject> tires)
    {
        var newPos = Extensions.CalculatePercentage(.52f, .7f, percentage);

        var mult = -1;

        foreach (var tire in tires)
        {
            tire.transform.localPosition = new Vector3(newPos * mult,
                tire.transform.localPosition.y, tire.transform.localPosition.z);

            mult *= -1;
        }
    }

    public void ChangeTireSize(float size)
    {
        percentage = size;
    }
}