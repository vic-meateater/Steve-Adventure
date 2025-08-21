using Zenject;

namespace SteveAdventure
{
    public sealed class GameOverViewInstaller : Installer<GameOverViewInstaller>
    {
        public override void InstallBindings()
        {
            Container
                .Bind<IFactory<IGameOverViewModel>>()
                .To<GameOverViewPresenterFactory>()
                .AsSingle();
        }
    }
}