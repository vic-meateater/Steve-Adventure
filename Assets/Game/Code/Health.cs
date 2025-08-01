using System;
using UnityEngine;

namespace SteveAdventure
{
    public sealed class Health
    {
        public event Action DeathEvent;
        public event Action<float> HealthChangedEvent;
        public float MaxHealth => _maxHealth;
        public float CurrentHealth => _currentHealth;
        
        private float _maxHealth;
        private float _currentHealth;

        public Health(float maxHealth)
        {
            _maxHealth = maxHealth;
            _currentHealth = maxHealth;
        }

        public void TakeDamage(float damage)
        {
            if (_currentHealth <= 0f)
                DeathEvent?.Invoke();
            ChangeValue(-damage);
            Debug.LogWarning($"Taking damage {damage} now {_currentHealth} HP left");
        }

        public void Heal(float healAmount)
        {
            if (_currentHealth + healAmount > _maxHealth)
            {
                _currentHealth = _maxHealth;
            }
            else
            {
                _currentHealth += healAmount;
            }
        }
        
        public void SetMaxHealth(float newMax)
        {
            _maxHealth = newMax;
            _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);
            HealthChangedEvent?.Invoke(_currentHealth);
        } 
        
        private void ChangeValue(float value)
        {
            _currentHealth = Mathf.Clamp(_currentHealth + value, 0f, _maxHealth);
            HealthChangedEvent?.Invoke(_currentHealth);
        }
    }
}