using System;
using UnityEngine;

namespace SteveAdventure
{
    public sealed class HealthComponent : MonoBehaviour, IDamageable, IHealable
    {
        public event Action<float> HealthChangedEvent;
        
        [SerializeField] private float _maxHealth = 100f;
        private Health _health;

        private void Awake()
        {
            _health = new Health(_maxHealth);
            _health.HealthChangedEvent += OnHealthChanged;
        }

        private void OnHealthChanged(float currentHealth)
        {
            HealthChangedEvent?.Invoke(currentHealth);
        }

        public void TakeDamage(float damage)
        {
            _health.TakeDamage(damage);
            if (_health.CurrentHealth <= 0f)
            {
                Destroy(gameObject);
            }
        }

        public void Heal(float healAmount)
        {
            _health.Heal(healAmount);
        }

        public void SetMaxHealth(float newMax) => _health.SetMaxHealth(newMax);

        public float GetMaxHealth() => _health.MaxHealth;
        public float GetCurrentHealth() => _health.CurrentHealth;
        
        private void OnDestroy()
        {
            _health.HealthChangedEvent -= OnHealthChanged;
        }
    }
}