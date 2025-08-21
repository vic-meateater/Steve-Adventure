using System;
using System.Collections;
using R3;
using UnityEngine;
using Zenject;

namespace SteveAdventure
{
    public sealed class EnemySpawner : IInitializable, IDisposable
    {
        private readonly EnemyConfig[] _enemyConfigs;
        private readonly EnemyFactory _enemyFactory;

        private readonly Enemy[] _activeEnemies;
        private DisposableBag _disposables;

        [Inject]
        public EnemySpawner(
            EnemyConfig[] enemyConfigs,
            EnemyFactory enemyFactory)
        {
            _enemyConfigs = enemyConfigs;
            _enemyFactory = enemyFactory;
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

            enemy.SetOnDiedCallback(respawnDelay => OnEnemyRespawn(configIndex, respawnDelay));
            _activeEnemies[configIndex] = enemy;
        }

        private void OnEnemyRespawn(int configIndex, float respawnDelay)
        {
            _activeEnemies[configIndex] = null;

            Observable.Timer(TimeSpan.FromSeconds(respawnDelay))
                .Subscribe(_ => SpawnEnemy(configIndex))
                .AddTo(ref _disposables);
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}