using SteveAdventure.Data;
using UnityEngine;
using Zenject;

namespace SteveAdventure
{
    public sealed class EnemyInstaller : MonoInstaller
    {
        [SerializeField] private EnemyConfig _enemyConfig;
        //[SerializeField] private Transform _parentTransform;
        
        public override void InstallBindings()
        {
            Container.Bind<EnemyConfig>().FromInstance(_enemyConfig).AsSingle().NonLazy();
            Container.Bind<EnemySpawner>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<Enemy>()
                .FromComponentInNewPrefab(_enemyConfig.Prefab)
                //.UnderTransform(_parentTransform)
                .AsSingle()
                .NonLazy();
        }
    }
}