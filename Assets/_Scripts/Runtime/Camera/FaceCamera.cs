using UnityEngine;

namespace Udar
{
    public class FaceCamera : MonoBehaviour
    {
        private Transform _camTransform;

        public void SetCamera(Camera camera)
        {
            _camTransform = camera.transform;
        }

        private void LateUpdate()
        {
            transform.LookAt(transform.position + _camTransform.rotation * Vector3.forward,
                _camTransform.rotation * Vector3.up);
        }


        [ContextMenu("Face To Camera")]
        public void FaceToCamera()
        {
            transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward,
               Camera.main.transform.rotation * Vector3.up);
        }
    }
}
