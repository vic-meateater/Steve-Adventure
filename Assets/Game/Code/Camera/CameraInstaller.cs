using Unity.Cinemachine;
using UnityEngine;
using Zenject;

namespace SteveAdventure.Camera
{
    public class CameraInstaller : MonoInstaller
    {
        [SerializeField] private CinemachineCamera _cinemachineCamera;
        public override void InstallBindings()
        {
            Container.Bind<CameraController>()
                .AsSingle()
                .WithArguments(_cinemachineCamera)
                .NonLazy();
        }
    }
}