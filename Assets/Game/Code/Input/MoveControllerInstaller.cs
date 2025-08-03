using Zenject;

namespace SteveAdventure
{
    public sealed class MoveControllerInstaller : Installer<MoveControllerInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<MoveController>()
                .AsSingle()
                .NonLazy();
        }
    }
}