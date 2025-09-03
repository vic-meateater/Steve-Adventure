using Zenject;

namespace SteveAdventure
{
    public sealed class SettingsViewInstaller : Installer<SettingsViewInstaller>
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