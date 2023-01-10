using UnityEngine;

namespace Game
{
    public class CanvasActivator : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField, Tooltip("The container that activate")] private GameObject _root;

        public void ChangeActive(bool isActive)
        {
            if (isActive)
                Activate();
            else
                Deactivate();
        }
        public void Activate()
        {
            _root.SetActive(true);
            _canvas.enabled = true;
        }
        public void Deactivate()
        {
            _canvas.enabled = false;
            _root.SetActive(false);
        }
    }
}
