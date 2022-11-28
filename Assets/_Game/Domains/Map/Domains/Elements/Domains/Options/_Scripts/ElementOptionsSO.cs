using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Map.Element.Options
{
    [CreateAssetMenu(fileName = "Element Options", menuName = "Game/Map/Options/New Options", order = 0)]
    public class ElementOptionsSO : ScriptableObject
    {
        [SerializeField] private List<ElementOptionWithButton> _optionsButtons;

        private readonly Dictionary<ElementOption, OptionButtonBase> _optionsDictionary
            = new Dictionary<ElementOption, OptionButtonBase>();

        private void OnEnable()
        {
            foreach (var option in _optionsButtons)
            {
                _optionsDictionary.Add(option.Option, option.Pfb);
            }
        }

        public List<OptionButtonBase> GetPrefabsByOptions(ElementOption options)
        {
            var buttonsPrefabsList = new List<OptionButtonBase>();

            foreach (var iterateOption in _optionsDictionary)
            {
                if (options.HasFlag(iterateOption.Key))
                    buttonsPrefabsList.Add(iterateOption.Value);
            }
            return buttonsPrefabsList;

        }
    }

    [Serializable]
    public class ElementOptionWithButton
    {
        [field: SerializeField] public ElementOption Option { get; private set; }
        [field: SerializeField] public OptionButtonBase Pfb { get; private set; }

    }
}
