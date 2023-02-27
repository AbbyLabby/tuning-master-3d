using System.Collections;
using BoomBit.HyperCasual;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadManager : MonoBehaviour
{
    [SerializeField] private Image image;

    public int levelIndex;
    
    private void Start()
    {
        HCSDK.StartHCSDK();
        StartCoroutine(LoadAsyncScene());
    }

    private IEnumerator LoadAsyncScene()
    {
        var level = SceneManager.LoadSceneAsync(levelIndex);

        while (level.progress < 1)
        {
            image.fillAmount = level.progress;
            yield return new WaitForEndOfFrame();
        }
    }
}