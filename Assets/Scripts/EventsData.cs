using UnityEngine;
using UnityEngine.Events;

public static class EventsData
{
    public static UnityEvent<Color> ChangeColorEvent { get; } = new();
    public static UnityEvent StartColorPickerEvent { get; } = new();
    public static UnityEvent<Texture> UpdateDecalEvent { get; } = new();
    public static UnityEvent<Mesh> ChooseSpoilerEvent { get; } = new();
    public static UnityEvent<Color> OnButtonColorEvent { get; } = new();
    public static UnityEvent<Material> ChooseTireEvent { get; } = new();
    public static UnityEvent GlassBrokenEvent { get; } = new();
    public static UnityEvent CableConnectedEvent { get; } = new();
}
