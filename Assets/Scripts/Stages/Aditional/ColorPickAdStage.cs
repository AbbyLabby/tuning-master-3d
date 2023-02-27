using UnityEngine;

public class ColorPickAdStage : MonoBehaviour
{
    [SerializeField] private GameObject canvasUI;

    private void OnEnable()
    {
        EventsData.StartColorPickerEvent.AddListener(StartColorPicker);
        EventsData.OnButtonColorEvent.AddListener(SendColor);
    }

    private void StartColorPicker()
    {
        canvasUI.SetActive(true);
    }

    private void SendColor(Color color)
    {
        EventsData.ChangeColorEvent.Invoke(color);
        canvasUI.SetActive(false);
    }

    private void OnDisable()
    {
        EventsData.StartColorPickerEvent.RemoveAllListeners();
        EventsData.OnButtonColorEvent.RemoveAllListeners();
    }
}