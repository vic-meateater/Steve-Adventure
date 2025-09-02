using System.Collections.Generic;
using UnityEngine;

namespace SteveAdventure
{
    public class CharacterConfig : ScriptableObject
    {
        [field: Header("Character info")]
        [field: SerializeField] public GameObject Prefab { get; private set; }
        [field: SerializeField] public Vector3 SpawnPoint { get; private set; }
        
        [field: Header("Character stats")]
        [field: SerializeField] public int MoveSpeed { get; private set; }
        [field: SerializeField] public int Damage { get; private set; }
        [field: SerializeField] public int Health { get; private set; }
        
        [field: Header("Character audio")]
        [field: SerializeField] public List<AudioClip> FootstepSounds { get; private set; }
        [field: SerializeField] public AudioClip HitSound { get; private set; }
        [field: SerializeField] public AudioClip PainSound { get; private set; }
        [field: SerializeField] public AudioClip DeathSound { get; private set; }
    }
}