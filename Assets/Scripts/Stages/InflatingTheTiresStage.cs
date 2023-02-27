using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class InflatingTheTiresStage : MonoBehaviour, IStage
{
    [SerializeField] private GameObject canvasUI;
    [SerializeField] private Transform pumpArrow;
    [SerializeField] private float speed;
    [SerializeField] private CinemachineVirtualCamera vCam;
    [SerializeField] private TMP_Text resultText;
    
    private readonly String[] resultStrings = {"Bad", "Not Bad", "Nice", "Very Good"};

    private List<SkinnedMeshRenderer> tiresMesh = new List<SkinnedMeshRenderer>();
    private List<SphereCollider> tiresColliders = new List<SphereCollider>();
    private List<GameObject> tirePumps = new List<GameObject>();
    private StageController m_StageController;

    private float count = 0;

    private int currentIndex = 0;

    private bool isPressed = false;

    private void CollectTire()
    {
        var tempTires = new List<GameObject>();

        foreach (var tire in CarManager.Singleton.GetCar().tires)
        {
            tempTires.Add(tire);
        }
        
        (tempTires[1], tempTires[2], tempTires[3]) = (tempTires[2], tempTires[3], tempTires[1]);
        
        foreach (var tire in tempTires)
        {
            tiresMesh.Add(tire.GetComponent<SkinnedMeshRenderer>());
            tiresColliders.Add(tire.GetComponent<SphereCollider>());
            tirePumps.Add(tire.transform.GetChild(0).gameObject);
        }
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    isPressed = true;
                    tirePumps[currentIndex].GetComponent<Animator>().SetBool("Pumping", true);
                    break;
                case TouchPhase.Ended:
                    CheckResult(count);
                    isPressed = false;
                    tirePumps[currentIndex].GetComponent<Animator>().SetBool("Pumping", false);
                    break;
            }
        }

        if (isPressed)
            CalculateRotation();
    }

    private void CalculateRotation()
    {
        if (count > 290)
        {
            BadResult();
            return;
        }

        pumpArrow.eulerAngles = new Vector3(pumpArrow.eulerAngles.x, pumpArrow.eulerAngles.y,
            pumpArrow.eulerAngles.z - speed * Time.deltaTime);

        CalculateTireFlat();

        count += speed * Time.deltaTime;
    }

    private void CalculateTireFlat()
    {
        var percent = count / (290 / 100);

        var colliderRadius = Extensions.CalculatePercentage(.26f, .33f, percent);

        tiresMesh[currentIndex].SetBlendShapeWeight(2, 100 - percent);

        tiresColliders[currentIndex].radius = colliderRadius;
    }

    private void SetDefaultArrowPos()
    {
        pumpArrow.eulerAngles = new Vector3(pumpArrow.eulerAngles.x, pumpArrow.eulerAngles.y, 145);
        count = 0;
    }

    private void RestoreTire()
    {
        tiresMesh[currentIndex].SetBlendShapeWeight(2, 100);

        tiresColliders[currentIndex].radius = .26f;
    }

    private void CheckResult(float result)
    {
        if (result < 180)
        {
            SetDefaultArrowPos();
            RestoreTire();

            resultText.text = resultStrings[1];
            
            return;
        }

        if (result < 230)
        {
            SuccessfulResult();

            resultText.text = resultStrings[Random.Range(2, 4)];
        }
        else
        {
            BadResult();

            resultText.text = resultStrings[0];
        }
    }

    private void SuccessfulResult()
    {
        Debug.LogWarning("Good");
        SetDefaultArrowPos();
        NextTire();
    }

    private void BadResult()
    {
        Debug.LogWarning("Bad");
        SetDefaultArrowPos();
        RestoreTire();
        NextTire();
    }

    private void NextTire()
    {
        if (currentIndex < 3)
        {
            currentIndex++;

            if (currentIndex == 2)
            {
                DragAndRotate.Singleton.Rotate(180, 2f);

                StartCoroutine(EnableOtherSide());
                return;
            }

            EnableNextTire();
        }
        else
        {
            StageEnd();
        }
    }

    private void EnableNextTire()
    {
        tirePumps[currentIndex].SetActive(true);

        vCam.m_Follow = tiresMesh[currentIndex].transform;
        vCam.m_LookAt = tiresMesh[currentIndex].transform;

        tirePumps[currentIndex - 1].SetActive(false);
    }

    private IEnumerator EnableOtherSide()
    {
        vCam.m_Follow = null;
        vCam.m_LookAt = null;
        
        yield return new WaitForSeconds(2f);
        
        EnableNextTire();
    }

    public void StageStart(StageController stageController)
    {
        m_StageController = stageController;
        canvasUI.SetActive(true);
        CollectTire();
        tirePumps[currentIndex].SetActive(true);
        
        vCam.m_Follow = tiresMesh[currentIndex].transform;
        vCam.m_LookAt = tiresMesh[currentIndex].transform;

        DragAndRotate.Singleton.EnableImportantStage();
        DragAndRotate.Singleton.SetRotation(0);
    }

    public void StageEnd()
    {
        canvasUI.SetActive(false);
        DragAndRotate.Singleton.DisableImportantStage();

        tirePumps[currentIndex].SetActive(false);

        m_StageController.EndStage();
    }
}