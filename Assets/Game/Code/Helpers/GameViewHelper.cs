using UnityEngine;
using Zenject;

namespace SteveAdventure
{
    public sealed class GameViewHelper : MonoBehaviour
    {
        [SerializeField] private GameView _gameView;
        [SerializeField] private GameViewConfig _gameViewConfig;
        
        private IFactory<IGameViewModel> _gameViewFactory;
        private IGameViewModel _gameViewModel;

        [Inject]
        private void Construct(IFactory<IGameViewModel> factory)
        {
            _gameViewFactory = factory;
        }

        private void Awake()
        {
            _gameViewModel ??= _gameViewFactory.Create();
            _gameView.Show(_gameViewModel);
        }
    }
}