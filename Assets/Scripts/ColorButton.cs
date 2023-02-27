using UnityEngine;
using UnityEngine.UI;

public class ColorButton : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Color color;

    private void Start()
    {
        image.color = color;
    }

    public void OnButtonClick()
    {
        EventsData.OnButtonColorEvent.Invoke(color);
    }
    
}
