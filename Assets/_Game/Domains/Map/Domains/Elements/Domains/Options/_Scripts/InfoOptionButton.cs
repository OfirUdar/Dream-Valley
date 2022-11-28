using UnityEngine;

namespace Game.Map.Element.Options
{
    public class InfoOptionButton : OptionButtonBase
    {
        [SerializeField] private PanelActivator _pfb;
        public override void Execute()
        {
            Instantiate(_pfb).Show();
        }
    }
}
