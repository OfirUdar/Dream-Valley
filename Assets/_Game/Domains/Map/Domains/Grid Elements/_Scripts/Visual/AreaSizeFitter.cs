using UnityEngine;
using Zenject;

namespace Game.Map.Grid.Element
{
    public class AreaSizeFitter
    {
        [Inject]
        public void Init(GridSettings gridSettings, MapElementData data,MeshRenderer meshRenderer)
        {
            var scale = meshRenderer.transform.localScale;
            scale.x = gridSettings.CellSize * data.Width;
            scale.z = gridSettings.CellSize * data.Height;

            meshRenderer.transform.localScale = scale;

            meshRenderer.material.mainTextureScale =
                new Vector2(data.Width, data.Height) / 2f;
        }


    }

}
