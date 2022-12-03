using Udar;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "Element", menuName = "Game/Map/Elements/New Element", order = 0)]
    public class MapElementSO : ScriptableObject
    {
        [field: SerializeField, ReadOnly] public string GUID { get; private set; }
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField, TextArea] public string Description { get; private set; }
        [field: SerializeField] public Sprite Sprite { get; private set; }
        [field: Space, SerializeField] public int Width { get; private set; }
        [field: SerializeField] public int Height { get; private set; }
        [field: SerializeField,Space] public ElementOption Options { get; private set; }
        
        [field: SerializeField] public GameObject Pfb { get; private set; }


#if UNITY_EDITOR
        [ContextMenu("Generate GUID")]
        public void GenerateGUID()
        {
            GUID = UnityEditor.GUID.Generate().ToString();
        }
        private void OnEnable()
        {
            if (UnityEditor.EditorApplication.isPlaying && string.IsNullOrEmpty(GUID))
                Debug.Log(Name + "<color=yellow> is missing guid - please generate for it", this);
        }
#endif
    }
}