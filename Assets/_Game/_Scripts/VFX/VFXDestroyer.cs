using System.Threading.Tasks;
using UnityEngine;

namespace Game
{
    public class VFXDestroyer : MonoBehaviour
    {
        [SerializeField] private float _lifeTime = 1f;

        private int _lifeTimeInMiliseconds;

        private void Awake()
        {
            _lifeTimeInMiliseconds = Mathf.RoundToInt(_lifeTime * 1000);
        }
        private async void OnEnable()
        {
            await Task.Delay(_lifeTimeInMiliseconds);

            gameObject.SetActive(false);
        }



    }

}