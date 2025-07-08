using UnityEngine;

namespace SteveAdventure
{
    public sealed class HealthComponent : MonoBehaviour, IDamageable, IHealable
    {
        [SerializeField] private float _maxHealth = 100f;
        private Health _health;

        private void Awake()
        {
            _health = new Health(_maxHealth);
        }

        public void TakeDamage(float damage)
        {
            _health.TakeDamage(damage);
            if (_health.CurrentHealth <= 0f)
            {
                Debug.Log("Entity died.");
            }
        }

        public void Heal(float healAmount)
        {
            _health.Heal(healAmount);
        }
        
        public void SetMaxHealth(float newMax)
        {
            _health.SetMaxHealth(newMax);
        }
    }
}