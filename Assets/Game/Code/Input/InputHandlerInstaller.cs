using UnityEngine;
using Zenject;

namespace SteveAdventure
{
    public sealed class InputHandlerInstaller : Installer<InputHandlerInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<InputHandler>()
                .AsSingle()
                .NonLazy();
        }
    }
}