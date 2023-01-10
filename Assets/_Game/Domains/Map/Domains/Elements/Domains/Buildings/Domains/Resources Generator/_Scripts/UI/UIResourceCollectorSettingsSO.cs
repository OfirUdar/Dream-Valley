using UnityEngine;

namespace Game.Map.Element.Building.Resources.UI
{
    [CreateAssetMenu(fileName = "Resource Collector settings", menuName = "Game/Map/Elements/Buildings/Resources/Resource Collector Settings")]
    public class UIResourceCollectorSettingsSO : ScriptableObject
    {
        [field: SerializeField] public Color DefaultColor { get; private set; }
        [field: SerializeField] public Color FullStorageColor { get; private set; }

    }
}