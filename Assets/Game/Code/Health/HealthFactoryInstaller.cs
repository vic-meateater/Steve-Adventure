using Zenject;

namespace SteveAdventure
{
    public sealed class HealthFactoryInstaller : Installer<HealthFactoryInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindIFactory<CharacterConfig, IHealthViewModel>()
                .FromFactory<HealthPresenterFactory>();
        }
    }
}