using UnityEngine;

namespace Game.Map
{
    public class VFXDestroyer : MonoBehaviour
    {
        [SerializeField] private float _lifeTime = 1f;

        private void Start()
        {
            Destroy(gameObject, _lifeTime);
        }

    }

}