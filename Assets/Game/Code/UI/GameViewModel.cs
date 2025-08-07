using UnityEngine;

namespace SteveAdventure
{
    public sealed class GameViewModel : IGameViewModel
    {
        public void SpawnEnemy()
        {
            Debug.Log("Spawn Enemy called in GameViewModel");
        }
    }
}