using UnityEngine;

public class ChargerBaseScript : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private PowerPluginStage powerPluginStage;

    private void OnMouseDown()
    {
        animator.SetTrigger("PressButton");
        powerPluginStage.CheckButtonPress();
    }
}
