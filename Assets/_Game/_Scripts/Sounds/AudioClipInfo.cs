using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "AudioClipInfo", menuName = "Game/Sounds/AudioClipInfo")]
    public class AudioClipInfo : ScriptableObject
    {
        [field: SerializeField] public AudioClip Clip { get; private set; }
        [field: SerializeField] public float Pitch { get; private set; } = 1f;
        [field: SerializeField, Range(0f, 1f)] public float ScaleVolume { get; private set; } = 1f;
    }
}
