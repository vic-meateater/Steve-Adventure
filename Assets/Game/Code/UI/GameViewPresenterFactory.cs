using Zenject;

namespace SteveAdventure
{
    public sealed class GameViewPresenterFactory : IFactory<IGameViewModel>
    {
        private readonly IFactory<IGamePausedViewModel> _gamePausedPresenterFactory;
        private readonly IFactory<IGameOverViewModel> _gameOverPresenterFactory;

        public GameViewPresenterFactory(
            IFactory<IGamePausedViewModel> gamePausedPresenterFactory,
            IFactory<IGameOverViewModel> gameOverPresenterFactory)
        {
            _gamePausedPresenterFactory = gamePausedPresenterFactory;
            _gameOverPresenterFactory = gameOverPresenterFactory;
        }
        
        public IGameViewModel Create()
        {
            var gamePausedViewModel = _gamePausedPresenterFactory.Create();
            var gameOverViewModel = _gameOverPresenterFactory.Create();
            return new GameViewModel(gamePausedViewModel, gameOverViewModel);
        }
    }
}