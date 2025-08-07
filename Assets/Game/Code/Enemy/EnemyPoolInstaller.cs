using SteveAdventure.Data;
using UnityEngine;
using Zenject;

namespace SteveAdventure
{
    public class EnemyPoolInstaller : MonoInstaller
    {
        [SerializeField] private Enemy _enemyPrefab;
        [SerializeField] private int _initialPoolSize = 10;
        [SerializeField] private int _maxPoolSize = 20;

        public override void InstallBindings()
        {
            Container.BindMemoryPool<Enemy, EnemyPool>()
                .WithInitialSize(_initialPoolSize)
                .WithMaxSize(_maxPoolSize)
                .FromComponentInNewPrefab(_enemyPrefab)
                .UnderTransformGroup("Enemy Pool");
            
            Container.BindFactory<EnemyConfig, Enemy, EnemyPoolFactory>()
                .FromPoolableMemoryPool();
        }
    }
}