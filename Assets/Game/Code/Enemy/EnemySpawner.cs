using System.Collections;
using UnityEngine;
using Zenject;

namespace SteveAdventure
{
    public sealed class EnemySpawner : IInitializable
    {
        private readonly EnemyConfig[] _enemyConfigs;
        private readonly EnemyFactory _enemyFactory;
        private readonly MonoBehaviour _coroutineRunner;

        private Enemy[] _activeEnemies;

        [Inject]
        public EnemySpawner(
            EnemyConfig[] enemyConfigs,
            EnemyFactory enemyFactory,
            MonoBehaviour coroutineRunner)
        {
            _enemyConfigs = enemyConfigs;
            _enemyFactory = enemyFactory;
            _coroutineRunner = coroutineRunner;
            _activeEnemies = new Enemy[_enemyConfigs.Length];
        }

        public void Initialize()
        {
            for (int i = 0; i < _enemyConfigs.Length; i++)
            {
                SpawnEnemy(i);
            }
        }

        private void SpawnEnemy(int configIndex)
        {
            var spawnConfig = _enemyConfigs[configIndex];
            var enemy = _enemyFactory.Create(spawnConfig);

            enemy.Died += () => OnEnemyDied(configIndex);

            _activeEnemies[configIndex] = enemy;
        }

        private void OnEnemyDied(int configIndex)
        {
            _activeEnemies[configIndex] = null;

            var respawnDelay = _enemyConfigs[configIndex].EnemySpawnConfig.ReSpawnDuration;
            _coroutineRunner.StartCoroutine(RespawnAfterDelay(configIndex, respawnDelay));
        }

        private IEnumerator RespawnAfterDelay(int configIndex, float delay)
        {
            yield return new WaitForSeconds(delay);
            SpawnEnemy(configIndex);
        }
    }
}