using Zenject;

namespace SteveAdventure
{
    public sealed class SettingsViewInstaller : Installer<SettingsViewInstaller>
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