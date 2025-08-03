using R3;

namespace SteveAdventure
{
    public interface IHealable
    {
        ReadOnlyReactiveProperty<float> CurrentHealth { get; }
        public void Heal(float healAmount);
    }
}
