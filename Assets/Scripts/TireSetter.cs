using UnityEngine;

public class TireSetter : MonoBehaviour
{
    [SerializeField] private Material tire;

    public void SendTireChoose()
    {
        EventsData.ChooseTireEvent.Invoke(tire);
    }
}
