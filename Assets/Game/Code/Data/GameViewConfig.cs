using TMPro;
using UnityEngine;

namespace SteveAdventure
{
    public sealed class GameViewConfig : ScriptableObject
    {
        [field: SerializeField] public TMP_Text TitleText { get; private set; }
    }
}