using UnityEngine;

namespace SteveAdventure
{
    [CreateAssetMenu(fileName = "EnemyConfig", menuName = "SteveGame/EnemyConfig")]
    public sealed class EnemyConfig : CharacterConfig
    {
        [field: Header("Enemy stats")]
        [field: SerializeField] public int AttackCooldown { get; private set; }
        [field: SerializeField] public EnemySpawnConfig EnemySpawnConfig { get; private set; }
    }
}