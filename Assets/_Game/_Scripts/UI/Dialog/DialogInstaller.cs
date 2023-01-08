using UnityEngine;
using Zenject;

namespace Game
{
    public class DialogInstaller : MonoInstaller
    {
        [SerializeField] private Dialog _dialogPfb;

        public override void InstallBindings()
        {
            Container.Bind<IDialog>().FromComponentInNewPrefab(_dialogPfb).AsSingle();
        }
    }
}
