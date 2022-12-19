using UnityEngine;
using Zenject;

namespace Game.Map.Element
{
    public class LevelsElementVisualHandler : MonoBehaviour, ILevelsElementVisualHandler
    {
        [SerializeField] private Transform _gfx;

        [Inject] private readonly LevelsListSO _levels;
        [Inject] private readonly ISelectVisual _selectVisual;


        public void Refresh(int level)
        {
            ClearGFX();

            var pfb = _levels[level].Pfb;

            GameObject.Instantiate(pfb, _gfx, false);

            _selectVisual.RefreshGFX();
        }

        private void ClearGFX()
        {
            var childCount = _gfx.childCount;
            for (int i = 0; i < childCount; i++)
            {
                var child = _gfx.GetChild(i);
                GameObject.Destroy(child.gameObject);
            }
        }
    }
}
