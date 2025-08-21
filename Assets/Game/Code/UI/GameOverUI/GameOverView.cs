using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SteveAdventure
{
    public sealed class GameOverView : MonoBehaviour, IGameOverView, IGameOverListener
    {
        [SerializeField] public TMP_Text _titleText;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _exitButton;

        private IGameOverViewModel _viewModel;
        private CanvasGroup _canvasGroup;
        private RectTransform _rectTransform;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _rectTransform = GetComponent<RectTransform>();

            _canvasGroup.alpha = 0f;
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;

            _rectTransform.localScale = Vector3.zero;
        }
        
        public void Init(IGameOverViewModel viewModel)
        {
            _viewModel = viewModel;
            gameObject.SetActive(false);
            GameCycleService.Instance.AddListener(this);
        }

        public void Show()
        {
            _viewModel.GameOver();
            _titleText.text = _viewModel.TitleText;
            gameObject.SetActive(true);
            
            AnimateView();
        }

        private void AnimateView()
        {
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;

            Sequence sequence = DOTween.Sequence();
            sequence
                .Append(_canvasGroup.DOFade(1f, 0.4f))
                .Join(_rectTransform.DOScale(Vector3.one, 0.6f).SetEase(Ease.OutBack));
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
            Show();
        }

        private void OnDisable()
        {
            _restartButton.onClick.RemoveListener(OnRestartButtonClicked);
            _exitButton.onClick.RemoveListener(OnExitButtonClicked);
        }
    }
}