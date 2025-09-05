using Zenject;

namespace SteveAdventure
{
    public sealed class MainMenuViewInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .Bind<IFactory<IAudioManager, ISettingsViewModel>>()
                .To<SettingsPresenterFactory>()
                .AsSingle();
        }
    }
}