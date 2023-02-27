using BoomBit.HyperCasual;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    // public void SetPaintColor(Color color)
    // {
    //     _isFirst = true;
    //     paintSphere.Color = color;
    //     paintFill.Color = color;
    //     _color = color;
    //     _isPaint = true;
    //     deodorantScript.ChangeColor(color);
    //     paintSphere.gameObject.SetActive(true);
    //     deodorantScript.gameObject.SetActive(true);
    // }
    //
    // public void SetSecondPaintColor(Color color)
    // {
    //     _isFirst = false;
    //     _time = 0;
    //     secondPaintSphere.Color = color;
    //     secondPintFill.Color = color;
    //     _color = color;
    //     _isPaint = true;
    //     deodorantScript.ChangeColor(color);
    //     secondPaintSphere.gameObject.SetActive(true);
    //     deodorantScript.gameObject.SetActive(true);
    // }

    // private void Update()
    // {
    //     if (Input.touchCount > 0 && _isPaint)
    //     {
    //         _time += Time.deltaTime;
    //         Debug.Log(_time);
    //     }
    //
    //     #region Debug
    //
    //     if (Input.GetMouseButton(0) && _isPaint)
    //     {
    //         _time += Time.deltaTime;
    //         Debug.Log(_time);
    //     }
    //
    //     #endregion
    //
    //     if (_time >= timeToPaint && _isPaint)
    //     {
    //         if (_isFirst)
    //         {
    //             paintFill.gameObject.SetActive(true);
    //             paintSphere.gameObject.SetActive(false);
    //             deodorantScript.gameObject.SetActive(false);
    //             menuManager.ShowDoneButton();
    //             _isPaint = false;
    //         }
    //         else
    //         {
    //             secondPintFill.gameObject.SetActive(true);
    //             secondPaintSphere.gameObject.SetActive(false);
    //             deodorantScript.gameObject.SetActive(false);
    //             menuManager.ShowSecondDoneButton();
    //             _isPaint = false;
    //         }
    //     }
    // }
}
