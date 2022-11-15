using Udar;
using UnityEngine;

namespace Game
{
    public class SceneConfig : MonoBehaviour
    {
        [SerializeField] private SceneCompositeSO _system;


        private void Awake()
        {
            SceneChanger.LoadAddtiveAsync(_system.Main.Names);
        }
    }
}
