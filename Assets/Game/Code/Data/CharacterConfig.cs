using UnityEngine;

namespace SteveAdventure
{
    public class CharacterConfig : ScriptableObject
    {
        [field: Header("Character info")]
        [field: SerializeField] public GameObject Prefab { get; private set; }
        
        [field: Header("Character stats")]
        [field: SerializeField] public int MoveSpeed { get; private set; }
        [field: SerializeField] public int Damage { get; private set; }
        [field: SerializeField] public int Health { get; private set; }
    }
}