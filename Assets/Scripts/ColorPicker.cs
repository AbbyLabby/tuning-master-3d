using DG.Tweening;
using UnityEngine;


public class ColorPicker : MonoBehaviour
{
    [SerializeField] private FlexibleColorPicker colorPicker;
  
    [SerializeField] private GameManager gameManager;

    private void Update()
    {
        //colorPreview.color = colorPicker.GetColor();

        if (Input.GetMouseButton(0))
        {
            UpdateColorPickerSize(.8f, .5f);
            return;
        }

        UpdateColorPickerSize(.45f, .5f);
    }

    private void UpdateColorPickerSize(float size, float duration)
    {
        if (transform.localScale != new Vector3(size, size, size))
            transform.DOScale(new Vector3(size, size, size), duration);
    }

    public void OnNextButtonClick(bool isFirst)
    {
        if (isFirst)
        {
           // gameManager.SetPaintColor(colorPreview.color);
        }
        else
        {
           // gameManager.SetSecondPaintColor(colorPreview.color);
        }
    }
}