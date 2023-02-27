using PaintIn3D;
using UnityEngine;

public class DeodorantScript : MonoBehaviour
{
    [SerializeField] private ParticleSystem particleColor;
    [SerializeField] private ParticleSystem particleColor1;
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private GameObject diskObj;

    [SerializeField] private float mainSpeed;

    [SerializeField] private Vector3 sprayOffset;

    public void ChangeColor(Color color)
    {
        particleColor.startColor = new Color(color.r, color.g, color.b, .6f);
        particleColor1.startColor = new Color(color.r, color.g, color.b, .6f);
        meshRenderer.materials[0].color = color;
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(
                P3dCommandSphere.Instance.Position.x + sprayOffset.x, P3dCommandSphere.Instance.Position.y + sprayOffset.y,
                P3dCommandSphere.Instance.Position.z + sprayOffset.z), mainSpeed * Time.deltaTime);

            transform.LookAt(P3dCommandSphere.Instance.Position);

            var touch = Input.GetTouch(0);

            CalculateTouchMoving(touch);
        }

        #region Debug

        if (Input.GetMouseButton(0))
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(
                P3dCommandSphere.Instance.Position.x + sprayOffset.x, P3dCommandSphere.Instance.Position.y + sprayOffset.y,
                P3dCommandSphere.Instance.Position.z + sprayOffset.z), mainSpeed * Time.deltaTime);

            transform.LookAt(P3dCommandSphere.Instance.Position);
        }

        #endregion
    }

    private void CalculateTouchMoving(Touch touch)
    {
        var screenCenter = Screen.width / 2f;
        var bias = (touch.position.x - screenCenter).Remap(-screenCenter, screenCenter, -100, 100);

        var r = diskObj.transform.rotation;
        if (bias < -4.5f)
            diskObj.transform.rotation =
                Quaternion.Euler(r.eulerAngles.x, r.eulerAngles.y + bias * Time.deltaTime, r.eulerAngles.z);
        if (bias > 4.5f)
            diskObj.transform.rotation =
                Quaternion.Euler(r.eulerAngles.x, r.eulerAngles.y + bias * Time.deltaTime, r.eulerAngles.z);
    }
}