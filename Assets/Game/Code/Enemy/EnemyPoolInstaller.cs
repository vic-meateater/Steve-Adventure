using System.Linq;
using UnityEngine;
using Zenject;

namespace SteveAdventure
{
    public class EnemyPoolInstaller : MonoInstaller
    {
        [SerializeField] private EnemyConfig _enemyConfig;
        [SerializeField] private EnemyConfig[] _enemyConfigs;
        [SerializeField] private int _initialPoolSize = 10;
        [SerializeField] private int _maxPoolSize = 20;

        public override void InstallBindings()
        {
            Container.Bind<EnemyConfig>().FromInstance(_enemyConfig).AsSingle().NonLazy();

            foreach (var enemyConfig in _enemyConfigs)
            {
                PoolBinding(enemyConfig);
            }
            //FactoryBinding();
            Container.Bind<EnemiesFactory>()
                .AsSingle()
                .NonLazy();
        }

        private void PoolBinding(EnemyConfig enemyConfig)
        {
            Container.BindMemoryPool<Enemy, EnemyPool>()
                .WithInitialSize(_initialPoolSize)
                .WithMaxSize(_maxPoolSize)
                // .FromSubContainerResolve()
                // .ByNewPrefabMethod(_enemyConfig.Prefab, sub =>
                // {
                //     sub.Bind<Enemy>().FromComponentOnRoot().AsTransient();
                //     sub.Bind<EnemyVision>().FromComponentOnRoot().AsTransient();
                //     sub.Bind<Mover>().FromComponentOnRoot().AsTransient();
                // })
                .FromComponentInNewPrefab(enemyConfig.Prefab)
                .UnderTransformGroup("Enemy Pool");
        }

        // private void FactoryBinding()
        // {
        //     Container.BindFactory<EnemyConfig, Enemy, EnemyPoolFactory>()
        //         .FromPoolableMemoryPool<EnemyConfig, Enemy, EnemyPool>(poolConfig => poolConfig
        //             .WithInitialSize(_initialPoolSize)
        //             .WithMaxSize(_maxPoolSize)
        //             // .FromSubContainerResolve()
        //             // .ByNewPrefabMethod(_enemyConfig.Prefab, sub =>
        //             // {
        //             //     sub.Bind<Enemy>().FromComponentOnRoot().AsTransient();
        //             //     sub.Bind<EnemyVision>().FromComponentOnRoot().AsTransient();
        //             //     sub.Bind<Mover>().FromComponentOnRoot().AsTransient();
        //             // })
        //             .FromComponentInNewPrefab(_enemyConfig.Prefab)
        //             .UnderTransformGroup("Enemy Pool"));
        // }
    }
}