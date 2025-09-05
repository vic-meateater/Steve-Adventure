using Zenject;

namespace SteveAdventure
{
    public sealed class GameViewPresenterFactory : IFactory<IGameViewModel>
    {
        private readonly IFactory<IGamePausedViewModel> _gamePausedPresenterFactory;
        private readonly IFactory<IGameOverViewModel> _gameOverPresenterFactory;
        private readonly IFactory<IAudioManager, ISettingsViewModel> _settingsViewModelFactory;
        private readonly IAudioManager _audioManager;

        public GameViewPresenterFactory(
            IFactory<IGamePausedViewModel> gamePausedPresenterFactory,
            IFactory<IGameOverViewModel> gameOverPresenterFactory,
            IFactory<IAudioManager, ISettingsViewModel> settingsViewModelFactory,
            IAudioManager audioManager)
        {
            _gamePausedPresenterFactory = gamePausedPresenterFactory;
            _gameOverPresenterFactory = gameOverPresenterFactory;
            _settingsViewModelFactory = settingsViewModelFactory;
            _audioManager = audioManager;
        }
        
        public IGameViewModel Create()
        {
            var gamePausedViewModel = _gamePausedPresenterFactory.Create();
            var gameOverViewModel = _gameOverPresenterFactory.Create();
            var settingsViewModel = _settingsViewModelFactory.Create(_audioManager);
            return new GameViewModel(gamePausedViewModel, gameOverViewModel, settingsViewModel);
        }
    }
}