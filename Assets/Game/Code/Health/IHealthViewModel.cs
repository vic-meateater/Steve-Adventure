using R3;

namespace SteveAdventure
{
    public interface IHealthViewModel : IDamageable, IHealable
    {
        ReadOnlyReactiveProperty<float> CurrentHealth { get; }
        ReadOnlyReactiveProperty<float> MaxHealth { get; }
        public ReadOnlyReactiveProperty<bool> IsDead { get; }
        
    }
}