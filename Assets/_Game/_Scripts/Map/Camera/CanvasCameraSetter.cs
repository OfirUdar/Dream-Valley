using UnityEngine;
using Zenject;

namespace Game
{
    public class CanvasCameraSetter : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;

        [Inject]
        public void Init(Camera camera)
        {
            _canvas.worldCamera = camera;
            Destroy(this);
        }
    }
}
