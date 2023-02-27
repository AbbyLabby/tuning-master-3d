using PaintIn3D;
using UnityEngine;
using UnityEngine.UI;

public class SliderValueUpdate : MonoBehaviour
{
    [SerializeField] private P3dChangeCounterText changeCounterText;
    [SerializeField] private Slider slider;
    
    private void Update()
    {
        if(slider.value == changeCounterText.percent)
            return;

        slider.value = changeCounterText.percent;
    }
}
