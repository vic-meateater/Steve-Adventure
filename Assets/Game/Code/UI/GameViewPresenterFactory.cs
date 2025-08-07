using Zenject;

namespace SteveAdventure
{
    public sealed class GameViewPresenterFactory : IFactory<GameViewModel>
    {
        public GameViewModel Create()
        {
            return new GameViewModel();
        }
    }
}