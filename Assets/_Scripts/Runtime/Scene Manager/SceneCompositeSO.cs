using Udar.SceneManager;
using UnityEngine;

namespace Udar
{
    [CreateAssetMenu(fileName = "Scene Composite", menuName = "Udar/Scene Manager/Scene Composite", order = 0)]
    public class SceneCompositeSO: ScriptableObject
    {
        public SceneFieldComposite Main;
    }
}