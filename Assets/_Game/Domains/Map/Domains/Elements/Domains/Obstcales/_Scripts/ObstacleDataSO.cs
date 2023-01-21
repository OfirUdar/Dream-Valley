using UnityEngine;

namespace Game.Map.Element.Obstcales
{
    [CreateAssetMenu(fileName = "Obstcale", menuName = "Game/Map/Elements/Obstcales/New Obstacle")]
    public class ObstacleDataSO: ScriptableObject
    {
        public MapElementSO Element;
        public ResourcePrice Price;
    }
}

