using Zenject;

namespace SteveAdventure
{
    public sealed class GameViewPresenterFactory : IFactory<GameViewModel>
    {
        private readonly DiContainer _container;

        [Inject]
        public GameViewPresenterFactory(DiContainer container)
        {
            _container = container;
        }

        public GameViewModel Create()
        {
            return _container.Instantiate<GameViewModel>();
        }
    }
}