using System.Threading.Tasks;
using UnityEngine;

namespace Game
{
    public interface ICameraController
    {
        public void SetActive(bool isActive);
        public void SetCanMove(bool isCanMove);
        public void SetCanZoom(bool isCanZoom);

        public Task FocusAsync(Vector3 position, float zoom);
    }
}