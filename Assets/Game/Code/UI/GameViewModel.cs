namespace SteveAdventure
{
    public sealed class GameViewModel : IGameViewModel
    {
        public IGamePausedViewModel GamePausedViewModel => _gamePausedViewModel;
        private IGamePausedViewModel _gamePausedViewModel;

        public IGameOverViewModel GameOverViewModel => _gameOverViewModel;
        private IGameOverViewModel _gameOverViewModel;

        public ISettingsViewModel SettingsViewModel => _settingsViewModel;
        private ISettingsViewModel _settingsViewModel;

        public GameViewModel(
            IGamePausedViewModel gamePausedViewModel,
            IGameOverViewModel gameOverViewModel,
            ISettingsViewModel settingsViewModel)
        {
            _gamePausedViewModel = gamePausedViewModel;
            _gameOverViewModel = gameOverViewModel;
            _settingsViewModel = settingsViewModel;
        }
    }
}