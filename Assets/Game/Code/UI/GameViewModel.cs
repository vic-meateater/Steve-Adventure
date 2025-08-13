using UnityEngine;
using Zenject;

namespace SteveAdventure
{
    public sealed class GameViewModel : IGameViewModel
    {
        [Inject]
        public void Construct()
        {
            Debug.Log("GameViewModel Constructed");
        }
    }
}