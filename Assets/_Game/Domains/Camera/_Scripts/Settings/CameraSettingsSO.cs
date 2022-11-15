using UnityEngine;

namespace Game.Camera
{
    [CreateAssetMenu(fileName = "Camera Settings", menuName = "Game/Camera/Settings", order = 0)]
    public class CameraSettingsSO : ScriptableObject
    {
        [SerializeField] private MoveSettings _moveSettings;
        [SerializeField] private ZoomSettings _zoomSettings;

        public MoveSettings MoveSettings => _moveSettings;
        public ZoomSettings ZoomSettings => _zoomSettings;
    }
}
