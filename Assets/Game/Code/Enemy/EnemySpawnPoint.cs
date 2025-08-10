using UnityEngine;
using Zenject;

namespace SteveAdventure
{
    public sealed class EnemySpawnPoint : MonoBehaviour
    {
        [Header("Что спавним")]
        [SerializeField] private EnemyConfig _config;

        [Header("Где спавним (если нужно переопределить конфиг)")]
        [SerializeField] private Transform _spawnPointOverride;
        [SerializeField] private Transform[] _waypointsOverride;
        [SerializeField] private float _waitDurationOverride = 0f;

        [Header("Через сколько секунд после смерти респаунить")]
        [SerializeField] private float _respawnDelay = 5f;

        private IFactory<EnemyConfig, Enemy> _factory;
        private Enemy _current;

        [Inject]
        public void Construct(IFactory<EnemyConfig, Enemy> factory)
        {
            _factory = factory;
        }

        private void Start()
        {
            SpawnNow();
        }

        private void SpawnNow()
        {
            //_current = _factory.Create(_config);
            Debug.Log("SpawnNow called in EnemySpawnPoint");
        }
    }
}