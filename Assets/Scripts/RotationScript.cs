using UnityEngine;

public class RotationScript : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 1;
    
    private void Update()
    {
        transform.Rotate(new Vector3(rotationSpeed, 0, 0));
    }
}
