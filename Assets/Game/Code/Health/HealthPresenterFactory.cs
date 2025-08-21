using Zenject;

namespace SteveAdventure
{
    public sealed class HealthPresenterFactory : IFactory<CharacterConfig, IHealthViewModel>
    {
        public IHealthViewModel Create(CharacterConfig config)
        {
            return new HealthViewModel(config);
        }
    }
}