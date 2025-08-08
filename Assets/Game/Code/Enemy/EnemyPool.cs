using Zenject;

namespace SteveAdventure
{
    public class EnemyPool : MonoMemoryPool<EnemyConfig, IMemoryPool, Enemy>
    {
        protected override void OnCreated(Enemy enemy)
        {
            enemy.gameObject.SetActive(false);
        }

        protected override void OnSpawned(Enemy enemy)
        {
            enemy.gameObject.SetActive(true);
        }

        protected override void OnDespawned(Enemy enemy)
        {
            enemy.gameObject.SetActive(false);
        }

        protected override void Reinitialize(EnemyConfig config, IMemoryPool pool, Enemy enemy)
        {
            enemy.transform.position = config.EnemySpawnConfig.SpawnPoint;
            enemy.OnSpawned(config, pool);
        }
    }

    public class EnemyPoolFactory : PlaceholderFactory<EnemyConfig, Enemy>
    {
    }
}