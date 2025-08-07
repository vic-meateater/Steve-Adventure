using SteveAdventure.Data;
using UnityEngine;
using Zenject;

namespace SteveAdventure
{
    public sealed class GameViewHelper : MonoBehaviour
    {
        [SerializeField] private GameView _gameView;
        [SerializeField] private GameViewConfig _gameViewConfig;
        
        private GameViewPresenterFactory _gameViewPresenterFactory;
        private IGameViewModel _gameViewModel;

        [Inject]
        private void Construct(GameViewPresenterFactory factory)
        {
            _gameViewPresenterFactory = factory;
        }

        private void Start()
        {
            _gameViewModel ??= _gameViewPresenterFactory.Create();
            _gameView.Show(_gameViewModel);
        }
    }
}