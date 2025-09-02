using UnityEngine;

namespace SteveAdventure
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "SteveGame/Player/PlayerConfig")]
    public sealed class PlayerConfig : CharacterConfig
    {
        [field: Header("Player audio")]
        [field: SerializeField] public AudioClip DashSound { get; private set; }
        [field: Header("Player stats")]
        [field: SerializeField] public int DashSpeed { get; private set; }
        
        
    }
}