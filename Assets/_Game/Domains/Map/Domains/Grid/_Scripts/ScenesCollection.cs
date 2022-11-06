using Udar;
using Udar.SceneField;
using UnityEngine;

namespace Game
{
    //Temp
    public class ScenesCollection : MonoBehaviour
    {
        [field: SerializeField] public SceneField MainUI { get; private set; }
        
        public void Start()
        {
            SceneChanger.LoadAddtiveAsync(MainUI.Name);
        }



    }

   
}