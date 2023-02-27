using UnityEngine;

public class SpoilerSetter : MonoBehaviour
{
    [SerializeField] private Mesh spoilerMesh;

    public void SendSpoilerEvent()
    {
        EventsData.ChooseSpoilerEvent.Invoke(spoilerMesh);
    }
}
