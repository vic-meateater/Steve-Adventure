using R3;
using UnityEngine;

namespace SteveAdventure
{
    public sealed class HealthViewModel : IHealthViewModel
    {
        public ReadOnlyReactiveProperty<float> CurrentHealth => _currentHealth;
        private readonly ReactiveProperty<float> _currentHealth;
        public ReadOnlyReactiveProperty<float> MaxHealth => _maxHealth;
        private readonly ReactiveProperty<float> _maxHealth;

        public HealthViewModel(CharacterConfig config)
        {
            Debug.Log("HealthViewModel created with config: " + config);
            _maxHealth = new ReactiveProperty<float>(config.Health);
            _currentHealth = new ReactiveProperty<float>(config.Health);
        }

        public void TakeDamage(float damage)
        {
            ChangeValue(-damage);
        }

        public void Heal(float amount)
        {
            ChangeValue(amount);
        }

        private void ChangeValue(float value)
        {
            float newHealth = Mathf.Clamp(_currentHealth.Value + value, 0f, _maxHealth.Value);
            _currentHealth.Value = newHealth;
        }
    }
}