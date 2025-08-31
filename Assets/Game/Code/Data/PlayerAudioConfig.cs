using System.Collections.Generic;
using UnityEngine;

namespace SteveAdventure
{
    [CreateAssetMenu(fileName = "PlayerAudioConfig", menuName = "SteveGame/Player/AudioConfig")]
    public sealed class PlayerAudioConfig : ScriptableObject
    {
        [field: SerializeField] public List<AudioClip> WalkSounds { get; private set; }
        [field: SerializeField] public AudioClip DashSound { get; private set; }
        [field: SerializeField] public AudioClip HitSound { get; private set; }
        [field: SerializeField] public AudioClip PainSound { get; private set; }
    }
}