using Zenject;

namespace SteveAdventure
{
    public sealed class GamePausePresenterFactory : IFactory<IGamePausedViewModel>
    {
        public IGamePausedViewModel Create()
        {
            return new GamePausedViewModel();
        }
    }
}