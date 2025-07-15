namespace SteveAdventure
{
    public interface IDamageable
    {
        void TakeDamage(float damage);
        public float GetCurrentHealth();
    }
}