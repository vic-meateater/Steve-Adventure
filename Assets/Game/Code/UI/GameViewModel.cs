using UnityEngine;
using Zenject;

namespace SteveAdventure
{
    public sealed class GameViewModel : IGameViewModel
    {
        private EnemySpawner _enemySpawner;
        
        [Inject]
        public void Construct(EnemySpawner enemySpawner)
        {
            Debug.Log("GameViewModel Constructed");
            _enemySpawner = enemySpawner;
        }
        public void SpawnEnemy()
        {
            var enemy = _enemySpawner.SpawnEnemy();
            Debug.Log($"Enemy spawned: {enemy.name}");
        }
    }
}