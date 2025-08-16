using Zenject;

namespace SteveAdventure
{
    public sealed class GamePausedViewInstaller : Installer<GamePausedViewInstaller>
    {
        public override void InstallBindings()
        {
            Container
                .Bind<IFactory<IGamePausedViewModel>>()
                .To<GamePausePresenterFactory>()
                .AsSingle();
        }
    }
}