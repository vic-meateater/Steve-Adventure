using R3;

namespace SteveAdventure
{
    public interface IHealthModel : IDamageable, IHealable
    {
        ReadOnlyReactiveProperty<float> CurrentHealth { get; }
        ReadOnlyReactiveProperty<float> MaxHealth { get; }
    }
}