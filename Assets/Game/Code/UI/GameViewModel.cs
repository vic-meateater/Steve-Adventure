namespace SteveAdventure
{
    public sealed class GameViewModel : IGameViewModel
    {
        public IGamePausedViewModel GamePausedViewModel => _gamePausedViewModel;
        private IGamePausedViewModel _gamePausedViewModel;

        public IGameOverViewModel GameOverViewModel => _gameOverViewModel;
        private IGameOverViewModel _gameOverViewModel;

        public GameViewModel(
            IGamePausedViewModel gamePausedViewModel,
            IGameOverViewModel gameOverViewModel)
        {
            _gamePausedViewModel = gamePausedViewModel;
            _gameOverViewModel = gameOverViewModel;
        }
    }
}