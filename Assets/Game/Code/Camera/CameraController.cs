using Unity.Cinemachine;
using UnityEngine;
using Zenject;

namespace SteveAdventure.Camera
{
    public sealed class CameraController
    {
        private CinemachineCamera _cinemachineCamera;
        private Player _player;

        [Inject]
        public void Create(CinemachineCamera cinemachineCamera, Player player)
        {
            Debug.Log("CameraController created");
            _cinemachineCamera = cinemachineCamera;
            _player = player;
            _cinemachineCamera.Follow = player.transform;
        }
    }
}