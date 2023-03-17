using Udar;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "Resource", menuName = "Game/Resources/New Resource", order = 0)]
    public class ResourceDataSO : ScriptableObject
    {
        [field: SerializeField, ReadOnly] public string GUID { get; private set; }
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public Sprite Sprite { get; private set; }
        [field: SerializeField] public AudioClipInfoSO CollectAudio { get; private set; }


#if UNITY_EDITOR
        [ContextMenu("Generate GUID")]
        public void GenerateGUID()
        {
            GUID = UnityEditor.GUID.Generate().ToString();
        }
        private void OnValidate()
        {
            if (UnityEditor.EditorApplication.isPlaying && string.IsNullOrEmpty(GUID))
                Debug.Log(Name + " is missing guid - please generate for it", this);
        }
#endif
    }
}