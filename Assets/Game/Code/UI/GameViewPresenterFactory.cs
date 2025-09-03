using Zenject;

namespace SteveAdventure
{
    public sealed class GameViewPresenterFactory : IFactory<IGameViewModel>
    {
        private readonly IFactory<IGamePausedViewModel> _gamePausedPresenterFactory;
        private readonly IFactory<IGameOverViewModel> _gameOverPresenterFactory;
        private readonly IFactory<ISettingsViewModel> _settingsViewModelFactory;

        public GameViewPresenterFactory(
            IFactory<IGamePausedViewModel> gamePausedPresenterFactory,
            IFactory<IGameOverViewModel> gameOverPresenterFactory,
            IFactory<ISettingsViewModel> settingsViewModelFactory)
        {
            _gamePausedPresenterFactory = gamePausedPresenterFactory;
            _gameOverPresenterFactory = gameOverPresenterFactory;
            _settingsViewModelFactory = settingsViewModelFactory;
        }
        
        public IGameViewModel Create()
        {
            var gamePausedViewModel = _gamePausedPresenterFactory.Create();
            var gameOverViewModel = _gameOverPresenterFactory.Create();
            var settingsViewModel = _settingsViewModelFactory.Create();
            return new GameViewModel(gamePausedViewModel, gameOverViewModel, settingsViewModel);
        }
    }
}