using UnityEngine;
using Zenject;

namespace Game
{
    public class DialogInstaller : MonoInstaller
    {
        [SerializeField] private Dialog _dialog;

        public override void InstallBindings()
        {
            Container.Bind<IDialog>().FromInstance(_dialog).AsSingle();
        }
    }
}
