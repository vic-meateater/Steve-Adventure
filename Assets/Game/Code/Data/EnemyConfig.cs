using UnityEngine;

namespace SteveAdventure
{
    [CreateAssetMenu(fileName = "EnemyConfig", menuName = "SteveGame/EnemyConfig")]
    public sealed class EnemyConfig : CharacterConfig
    {
        [field: Header("Enemy stats")]
        [field: SerializeField] public int AttackCooldown { get; private set; }

        [field: Header("Enemy AI")]
        [field: SerializeField] public float RespawnDuration { get; private set; }
        [field: SerializeField] public Vector3[] Waypoints { get; private set; }
        [field: SerializeField] public float WaitDuration { get; private set; }
    }
}