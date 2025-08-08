using UnityEngine;
using Zenject;

namespace SteveAdventure
{
    public class EnemyPoolInstaller : MonoInstaller
    {
        [SerializeField] private EnemyConfig _enemyConfig;
        [SerializeField] private int _initialPoolSize = 10;
        [SerializeField] private int _maxPoolSize = 20;

        public override void InstallBindings()
        {
            Container.Bind<EnemyConfig>().FromInstance(_enemyConfig).AsSingle().NonLazy();
            Container.BindMemoryPool<Enemy, EnemyPool>()
                .WithInitialSize(_initialPoolSize)
                .WithMaxSize(_maxPoolSize)
                .FromComponentInNewPrefab(_enemyConfig.Prefab)
                .UnderTransformGroup("Enemy Pool");
            
            Container.BindFactory<EnemyConfig, Enemy, EnemyPoolFactory>()
                .FromPoolableMemoryPool<EnemyConfig, Enemy, EnemyPool>(
                    poolConfig => poolConfig 
                        .WithInitialSize(_initialPoolSize)
                        .WithMaxSize(_maxPoolSize)
                        .FromComponentInNewPrefab(_enemyConfig.Prefab)
                        .UnderTransformGroup("Enemy Pool"));
            
            Container.Bind<EnemyVision>().FromComponentOnRoot().AsTransient();
        }
    }
}