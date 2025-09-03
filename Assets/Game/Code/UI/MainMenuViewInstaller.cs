using Zenject;

namespace SteveAdventure
{
    public sealed class MainMenuViewInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .Bind<IFactory<ISettingsViewModel>>()
                .To<SettingsPresenterFactory>()
                .AsSingle();
        }
    }
}