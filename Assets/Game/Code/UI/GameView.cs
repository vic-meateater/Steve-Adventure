using System;
using R3;
using UnityEngine;
using UnityEngine.UI;

namespace SteveAdventure
{
    public class GameView : MonoBehaviour
    {
        [SerializeField] private GamePauseView _pauseView;
        [SerializeField] private GameOverView _gameOverView;
        [SerializeField] private SettingsView _settingsView;

        [SerializeField] private Button _pauseButton;
        [SerializeField] private Button _settingsButton;

        private IGameViewModel _gameViewModel;
        private IGamePausedViewModel _gamePausedViewModel;
        private IGameOverViewModel _gameOverViewModel;
        private ISettingsViewModel _settingsViewModel;

        public void Show(IViewModel viewModel)
        {
            if (viewModel is not IGameViewModel gameViewModel)
                throw new Exception("ViewModel is not of type IGameViewModel");

            _gameViewModel = gameViewModel;
            _gamePausedViewModel = gameViewModel.GamePausedViewModel;
            _gameOverViewModel = gameViewModel.GameOverViewModel;
            _settingsViewModel = gameViewModel.SettingsViewModel;

            _pauseView.Init(_gamePausedViewModel);
            _gameOverView.Init(_gameOverViewModel);
            _settingsView.Init(_settingsViewModel);

            _gamePausedViewModel.IsVisible.Subscribe(isVisible => _pauseButton.gameObject.SetActive(!isVisible))
                .AddTo(this);
            _gameOverViewModel.IsVisible.Subscribe(isVisible => _pauseButton.gameObject.SetActive(!isVisible))
                .AddTo(this);
        }

        private void OnEnable()
        {
            _pauseButton.onClick.AddListener(OnPauseButtonClicked);
            _settingsButton.onClick.AddListener(OnSettingsButtonClicked);
        }

        private void OnPauseButtonClicked()
        {
            _pauseView.Show();
        }

        private void OnSettingsButtonClicked()
        {
            _settingsView.Show();
        }

        private void OnDestroy()
        {
            _pauseButton.onClick.RemoveListener(OnPauseButtonClicked);
            _settingsButton.onClick.RemoveListener(OnSettingsButtonClicked);
        }
    }
}