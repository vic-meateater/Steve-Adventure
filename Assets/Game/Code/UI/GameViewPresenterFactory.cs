using Zenject;

namespace SteveAdventure
{
    public sealed class GameViewPresenterFactory : IFactory<IGameViewModel>
    {
        private readonly IFactory<IGamePausedViewModel> _gamePausedPresenterFactory;

        public GameViewPresenterFactory(
            IFactory<IGamePausedViewModel> gamePausedPresenterFactory)
        {
            _gamePausedPresenterFactory = gamePausedPresenterFactory;
        }
        
        public IGameViewModel Create()
        {
            var gamePausedViewModel = _gamePausedPresenterFactory.Create();
            return new GameViewModel(gamePausedViewModel);
        }
    }
}