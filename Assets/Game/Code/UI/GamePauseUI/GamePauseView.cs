using R3;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SteveAdventure
{
    public class GamePauseView : MonoBehaviour, IGamePauseView
    {
        public ReadOnlyReactiveProperty<bool> IsVisible => _viewModel.IsVisible;

        [SerializeField] public TMP_Text _titleText;
        [SerializeField] private Button _resumeButton;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _exitButton;

        private IGamePausedViewModel _viewModel;

        public void Init(IGamePausedViewModel viewModel)
        {
            _viewModel = viewModel;
            gameObject.SetActive(false);
        }

        public void Show()
        {
            _viewModel.PauseGame();
            _titleText.text = _viewModel.TitleText;
            gameObject.SetActive(true);
        }

        private void OnEnable()
        {
            _resumeButton.onClick.AddListener(OnResumeButtonClicked);
            _restartButton.onClick.AddListener(OnRestartButtonClicked);
            _exitButton.onClick.AddListener(OnExitButtonClicked);
        }

        private void OnDisable()
        {
            _resumeButton.onClick.RemoveListener(OnResumeButtonClicked);
            _exitButton.onClick.RemoveListener(OnExitButtonClicked);
        }

        private void OnResumeButtonClicked()
        {
            _viewModel.ResumeGame();
            gameObject.SetActive(false);
        }
        
        private void OnRestartButtonClicked()
        {
            _viewModel.RestartGame();
            gameObject.SetActive(false);
        }

        private void OnExitButtonClicked()
        {
            _viewModel.ExitGame();
        }

    }
}