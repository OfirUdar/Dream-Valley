using UnityEngine;

namespace Game.Map.Element
{
    public class AreaSizeFitter
    {
        public AreaSizeFitter(GridSettingsSO gridSettings, MapElementSO data,
            Transform idleArea,
            MeshRenderer editArea,
            BoxCollider collider, Transform[] _arrows)
        {
            var colliderHeight = .4f;

            var scale = editArea.transform.localScale;
            scale.x = gridSettings.CellSize * data.Width;
            scale.z = gridSettings.CellSize * data.Height;


            idleArea.transform.localScale = scale;
            editArea.transform.localScale = scale;

            scale.y = colliderHeight;
            collider.size = scale;
            collider.center = Vector3.up * colliderHeight / 2f;


            editArea.material.mainTextureScale =
                new Vector2(data.Width, data.Height) / 2f;

            //Arrows:
            var offset = -0.1f;
            var position = _arrows[0].transform.position;
            position.x = scale.x / 2f + offset;
            _arrows[0].transform.position = position;


            position = _arrows[1].transform.position;
            position.x = -scale.x / 2f - offset;
            _arrows[1].transform.position = position;


            position = _arrows[2].transform.position;
            position.z = scale.z / 2f + offset;
            _arrows[2].transform.position = position;


            position = _arrows[3].transform.position;
            position.z = -scale.z / 2f - offset;
            _arrows[3].transform.position = position;

        }


    }

}
