using Zenject;

namespace SteveAdventure
{
    public sealed class GameViewInstaller : Installer<GameViewInstaller>
    {
        public override void InstallBindings()
        {
            //Container.BindInterfacesAndSelfTo<GameViewPresenterFactory>().AsSingle().NonLazy();
            Container.Bind<IFactory<IGameViewModel>>()
                .To<GameViewPresenterFactory>()
                .AsSingle()
                .NonLazy();
        }
    }
}