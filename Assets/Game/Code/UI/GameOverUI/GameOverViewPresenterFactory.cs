using Zenject;

namespace SteveAdventure
{
    public sealed class GameOverViewPresenterFactory : IFactory<IGameOverViewModel>
    {
        public IGameOverViewModel Create()
        {
            return new GameOverViewModel();
        }
    }
}