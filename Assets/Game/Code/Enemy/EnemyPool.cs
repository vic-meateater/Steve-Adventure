using SteveAdventure.Data;
using Zenject;

namespace SteveAdventure
{
    public class EnemyPool : MonoMemoryPool<EnemyConfig, Enemy>
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

        protected override void Reinitialize(EnemyConfig config, Enemy enemy)
        {
            enemy.transform.position = config.EnemySpawnConfig.SpawnPoint;
            enemy.Initialize(config);
        }
    }

    public class EnemyPoolFactory : PlaceholderFactory<EnemyConfig, Enemy>
    {
    }
}