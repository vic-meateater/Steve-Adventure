using UnityEngine;
using Zenject;

namespace SteveAdventure
{
    public sealed class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private PlayerConfig _playerConfig;
        [SerializeField] private Transform _playerSpawnPoint;
        [SerializeField] private PlayerUIView _playerUIView;
        
        public override void InstallBindings()
        {
            BindPlayer();
            BindPlayerDependencies();
        }

        private void BindPlayer()
        {
            Container.Bind<PlayerConfig>().FromInstance(_playerConfig).AsSingle().NonLazy();
            Container.Bind<PlayerUIView>().FromInstance(_playerUIView).AsSingle().NonLazy();
            Container.BindIFactory<PlayerConfig, IHealthViewModel>()
                .FromFactory<HealthPresenterFactory>();
            
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