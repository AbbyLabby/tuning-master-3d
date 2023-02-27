using UnityEngine;

public class CursorScript : MonoBehaviour
{
    [SerializeField] private GameObject cursor;

    private Camera _mainCamera;

    private void Start()
    {
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (cursor.activeInHierarchy)
            {
                cursor.SetActive(false);
            }
            else
            {
                cursor.SetActive(true);
            }
        }

        if (cursor.activeInHierarchy) cursor.transform.position = Input.mousePosition;
    }
}