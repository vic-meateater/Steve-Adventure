using Unity.Cinemachine;
using UnityEngine;
using Zenject;

namespace SteveAdventure
{
    public class SceneReferencesInstaller : MonoInstaller
    {
        [SerializeField] private CinemachineCamera _cinemachineCamera;
        [SerializeField] private EnemySpawnPoint _enemySpawnPoint;

        public override void InstallBindings()
        {
            Container.Bind<CinemachineCamera>()
                .FromInstance(_cinemachineCamera)
                .AsSingle()
                .NonLazy();

            Container.Bind<EnemySpawnPoint>()
                .FromInstance(_enemySpawnPoint)
                .AsSingle()
                .NonLazy();
        }
    }
}