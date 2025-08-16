using UnityEngine;
using Zenject;

namespace SteveAdventure
{
    public sealed class GameViewModel : IGameViewModel
    {
        public IGamePausedViewModel GamePausedViewModel => _gamePausedViewModel;
        private IGamePausedViewModel _gamePausedViewModel;

        public GameViewModel(IGamePausedViewModel gamePausedViewModel)
        {
            _gamePausedViewModel = gamePausedViewModel;
        }
    }
}