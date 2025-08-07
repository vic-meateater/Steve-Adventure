using SteveAdventure.Data;
using UnityEngine;
using Zenject;

namespace SteveAdventure
{
    public sealed class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private PlayerConfig _playerConfig;
        [SerializeField] private Transform _playerSpawnPoint;
        
        public override void InstallBindings()
        {
            BindPlayer();
            BindPlayerDependencies();
        }

        private void BindPlayer()
        {
            Container.Bind<PlayerConfig>().FromInstance(_playerConfig).AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<Player>()
                .FromComponentInNewPrefab(_playerConfig.Prefab)
                .UnderTransform(_playerSpawnPoint)
                .AsSingle()
                .NonLazy();
        }
        
        private void BindPlayerDependencies()
        {
            Container.Bind<PlayerVision>()
                .FromComponentInHierarchy() 
                .AsTransient();
        }
    }
}