using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace Game
{
    public class CameraSetter : MonoBehaviour
    {
        public UnityEvent<Camera> CameraDefined;

        [Inject]
        public void Init(Camera camera)
        {
            CameraDefined.Invoke(camera);
        }
    }
}