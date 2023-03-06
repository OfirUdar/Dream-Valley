using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Game
{
    public class VFX : MonoBehaviour
    {
        [SerializeField] private float _lifeTime = 1f;

        private WaitForSeconds _waitForSeconds;

        private void Awake()
        {
            _waitForSeconds = new WaitForSeconds(_lifeTime);
        }
        private void OnEnable()
        {
            StartCoroutine(HideDelayCoroutine());
        }
        private IEnumerator HideDelayCoroutine()
        {
            yield return _waitForSeconds;
            gameObject.SetActive(false);
        }

        public class Factory : PlaceholderFactory<Object, VFX>
        {

        }
    }

    public interface ICustomMonoPool<T> where T : MonoBehaviour
    {
        public T Spawn(T prefab);
        public void Despawn(T instance);
        public void Despawn(GameObject instance);
    }

}