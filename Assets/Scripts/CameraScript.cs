using System;
using DG.Tweening;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public static CameraScript Singleton { get; private set; }

    private Vector3 defaultPosition = new Vector3();
    private Vector3 defaultRotation = new Vector3();

    private Vector3 targetLookAt = new Vector3();
    private bool isLookAt = false;

    private void Awake()
    {
        Singleton = this;

        defaultPosition = transform.position;
        defaultRotation = transform.eulerAngles;
    }

    private void Update()
    {
        if(!isLookAt)
            return;
        
        transform.LookAt(targetLookAt);
    }

    public void MoveCamera(Vector3 targetPosition, float time)
    {
        transform.DOMove(targetPosition, time);
    }

    public void StartLookAtCamera(Vector3 targetPosition)
    {
        targetLookAt = targetPosition;

        isLookAt = true;
    }
    
    public void StopLookAtCamera()
    {
        isLookAt = false;
    }

    public void SetCameraPosition(Vector3 targetPosition)
    {
        transform.position = targetPosition;
    }

    public void SetDefaultPosition()
    {
        transform.position = defaultPosition;
    }

    public void SetDefaultRotation()
    {
        transform.eulerAngles = defaultRotation;
    }

    public void MoveDefaultPosition(float time)
    {
        transform.DOMove(defaultPosition, time);
    }
}
