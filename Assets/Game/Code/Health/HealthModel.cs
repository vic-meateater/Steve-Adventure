using R3;
using UnityEngine;

namespace SteveAdventure
{
    public sealed class HealthModel : IDamageable, IHealable
    {
        public ReadOnlyReactiveProperty<float> CurrentHealth => _currentHealth;
        private readonly ReactiveProperty<float> _currentHealth;
        public ReadOnlyReactiveProperty<float> MaxHealth => _maxHealth;
        private readonly ReactiveProperty<float> _maxHealth = new();

        public HealthModel(float maxHealth)
        {
            _maxHealth.Value = maxHealth;
            _currentHealth = new ReactiveProperty<float>(maxHealth);
        }

        public void TakeDamage(float damage)
        {
            ChangeValue(-damage);
        }

        public float GetCurrentHealth() => CurrentHealth.CurrentValue;

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