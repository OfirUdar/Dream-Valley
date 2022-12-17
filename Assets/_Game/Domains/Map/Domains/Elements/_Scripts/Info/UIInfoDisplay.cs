using UnityEngine;

namespace Game.Map.Element
{
    public abstract class UIInfoDisplay<T> : MonoBehaviour
    {
        [SerializeField] protected PanelActivator _panelActivator;

        public abstract void Setup(T t);

        public void Display(T t)
        {
            Setup(t);
            _panelActivator.Show();
        }

    }

}