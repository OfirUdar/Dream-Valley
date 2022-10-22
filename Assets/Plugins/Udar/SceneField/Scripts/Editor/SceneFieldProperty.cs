
using UnityEditor;
using UnityEngine;

namespace Udar.SceneField.Editor
{
    [CustomPropertyDrawer(typeof(SceneField))]
    public class SceneFieldProperty : PropertyDrawer
    {
        private SceneAsset _sceneAsset;
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var sceneFieldSP = property.FindPropertyRelative("_sceneOB");

            if (_sceneAsset == null && sceneFieldSP.objectReferenceValue != null)
            {
                _sceneAsset = sceneFieldSP.objectReferenceValue as SceneAsset;
            }


            _sceneAsset = EditorGUI.ObjectField(position, property.displayName, _sceneAsset, typeof(SceneAsset), false) as SceneAsset;

            sceneFieldSP.objectReferenceValue = _sceneAsset;

            property.serializedObject.ApplyModifiedProperties();
        }
    }
}

