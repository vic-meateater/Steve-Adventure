using Unity.Cinemachine;
using UnityEngine;
using Zenject;

namespace SteveAdventure.Helpers
{
    public class SceneReferencesInstaller : MonoInstaller
    {
        [SerializeField] private CinemachineCamera _cinemachineCamera;

        public override void InstallBindings()
        {
            Container.Bind<CinemachineCamera>()
                .FromInstance(_cinemachineCamera)
                .AsSingle()
                .NonLazy();
        }
    }
}