using Udar;
using UnityEngine;

namespace Game
{
    public class CanvasCameraSetter : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;

        private void Awake()
        {
            _canvas.worldCamera = CameraUtils.Main;
        }
    }
}
