using UnityEngine;
using Zenject;

namespace Game.Map.Element
{
    public class AreaSizeFitter
    {
        [Inject]
        public void Init(GridSettingsSO gridSettings, MapElementData data,
            Transform idleArea,
            MeshRenderer editArea,
            BoxCollider collider)
        {
            var scale = editArea.transform.localScale;
            scale.x = gridSettings.CellSize * data.Width;
            scale.z = gridSettings.CellSize * data.Height;

            idleArea.transform.localScale = scale;
            editArea.transform.localScale = scale;
            collider.size = scale;


            editArea.material.mainTextureScale =
                new Vector2(data.Width, data.Height) / 2f;

        }


    }

}
