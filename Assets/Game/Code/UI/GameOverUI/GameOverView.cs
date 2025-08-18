using R3;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SteveAdventure
{
    public sealed class GameOverView : MonoBehaviour, IGameOverView, IGameOverListener
    {
        public ReadOnlyReactiveProperty<bool> IsVisible => _viewModel.IsVisible;

        [SerializeField] public TMP_Text _titleText;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _exitButton;

        private IGameOverViewModel _viewModel;

        public void Init(IGameOverViewModel viewModel)
        {
            _viewModel = viewModel;
            gameObject.SetActive(false);
            GameCycleService.Instance.AddListener(this);
        }

        public void Show()
        {
            Debug.Log("Game Over View Show");
            _viewModel.GameOver();
            _titleText.text = _viewModel.TitleText;
            gameObject.SetActive(true);
        }

        private void OnEnable()
        {
            _restartButton.onClick.AddListener(OnRestartButtonClicked);
            _exitButton.onClick.AddListener(OnExitButtonClicked);
        }

        private void OnRestartButtonClicked()
        {
            _viewModel.RestartGame();
        }

        private void OnExitButtonClicked()
        {
            _viewModel.ExitGame();
        }

        public void OnGameOver()
        {
            Debug.Log("GAme Over View Is here");
            Show();
        }
    }
}