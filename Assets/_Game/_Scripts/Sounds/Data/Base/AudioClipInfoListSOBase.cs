using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public abstract class AudioClipInfoListSOBase<T> : ScriptableObject
    {
        [SerializeField] private AudioClipInfoWithEvent<T>[] _clips;

        private readonly Dictionary<T, AudioClipInfoSO> _clipsByEventsDictionary
            = new Dictionary<T, AudioClipInfoSO>();

        private void OnEnable()
        {
            for (int i = 0; i < _clips.Length; i++)
            {
                var clipInfoWithEvent = _clips[i];
                _clipsByEventsDictionary.Add(clipInfoWithEvent.Event, clipInfoWithEvent.AudioInfo);
            }
        }

        public AudioClipInfoSO GetByEvent(T eventT)
        {
            return _clipsByEventsDictionary[eventT];
        }
    }
    [Serializable]
    public class AudioClipInfoWithEvent<T>
    {
        [field: SerializeField] public T Event { get; private set; }
        [field: SerializeField] public AudioClipInfoSO AudioInfo { get; private set; }
    }
}
