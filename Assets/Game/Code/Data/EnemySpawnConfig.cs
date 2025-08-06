using UnityEngine;

namespace SteveAdventure.Data
{
    [CreateAssetMenu(fileName = "EnemySpawnConfig", menuName = "SteveGame/EnemySpawnConfig")]
    public sealed class EnemySpawnConfig  : ScriptableObject
    {
        [field: SerializeField] public Transform SpawnPoint { get; private set; }
        [field: SerializeField] public Transform[] Waypoints { get; private set; }
        [field: SerializeField] public float Duration { get; private set; }
    }
}