using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace SteveAdventure
{
    public class EnemyPoolInstaller : MonoInstaller
    {
        [SerializeField] private EnemyConfig[] _enemiesConfigs;
        [SerializeField] private int _initialPoolSize = 10;
        [SerializeField] private int _maxPoolSize = 20;

        public override void InstallBindings()
        {
            Container.Bind<EnemyConfig[]>().FromInstance(_enemiesConfigs).AsSingle();

            CreateEnemyPool();
            
            Container.Bind<EnemyFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<EnemySpawner>().AsSingle().NonLazy();
            Container.Bind<MonoBehaviour>().FromComponentInHierarchy().AsSingle();


        }

        private void CreateEnemyPool()
        {
            var configs = new HashSet<string>();
            
            foreach (var config in _enemiesConfigs)
            {
                var enemyConfigName = config.name;
                
                if (!configs.Contains(enemyConfigName))
                {
                    CreateEnemy(config);
                    configs.Add(enemyConfigName);
                }
            }
        }

        private void CreateEnemy(EnemyConfig enemyConfig)
        {
            Container.BindMemoryPool<Enemy, EnemyPool>()
                .WithId(enemyConfig.name)
                .WithInitialSize(_initialPoolSize)
                .WithMaxSize(_maxPoolSize)
                .FromComponentInNewPrefab(enemyConfig.Prefab)
                .UnderTransformGroup($"Enemy Pool - {enemyConfig.name}");
        }
    }
}