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
        public ReadOnlyReactiveProperty<bool> IsDead => _isDead;
        private readonly ReactiveProperty<bool> _isDead;
        
        public string HealthText => $"{CurrentHealth} / {MaxHealth}";

        private GameCycle _gameCycle;

        public HealthViewModel(CharacterConfig config)
        {
            Debug.Log("HealthViewModel created with config: " + config);
            _maxHealth = new ReactiveProperty<float>(config.Health);
            _currentHealth = new ReactiveProperty<float>(config.Health);
            _isDead = new ReactiveProperty<bool>(false);
            _currentHealth.Subscribe(health => _isDead.Value = health <= 0);
        }

        public void TakeDamage(float damage)
        {
            ChangeValue(-damage);
            Debug.Log(_currentHealth.Value);
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