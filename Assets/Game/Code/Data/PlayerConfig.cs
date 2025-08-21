using UnityEngine;

namespace SteveAdventure
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "SteveGame/PlayerConfig")]
    public sealed class PlayerConfig : CharacterConfig
    {
        [field: Header("Player stats")]
        [field: SerializeField] public int DashSpeed { get; private set; }
    }
}