using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SteveAdventure
{
    public class GameView : MonoBehaviour
    {
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _pauseButton;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _exitButton;
        [SerializeField] private Canvas _pauseCanvas;
        [SerializeField] private Slider _hpBar;
        private HealthComponent _playerHealth;

        private void Start()
        {
            var player = FindFirstObjectByType<Player>();

            if (player != null)
            {
                _playerHealth = player.GetComponent<HealthComponent>();
                if (_playerHealth != null)
                {
                    _playerHealth.HealthChangedEvent += OnHealthChanged;
                    _hpBar.maxValue = _playerHealth.GetMaxHealth();
                    _hpBar.value = _playerHealth.GetCurrentHealth();
                }
            }
        }

        private void OnHealthChanged(float currentHealth)
        {
            _hpBar.value = currentHealth;
        }

        private void OnEnable()
        {
            _playButton.onClick.AddListener(OnPlayButtonClicked);
            _pauseButton.onClick.AddListener(OnPauseButtonClicked);
            _restartButton.onClick.AddListener(OnRestartButtonClicked);
            _exitButton.onClick.AddListener(OnExitButtonClicked);
        }

        private void OnPlayButtonClicked()
        {
            GameCycleService.Instance?.ResumeGame();
            _pauseCanvas.gameObject.SetActive(false);
            _pauseButton.gameObject.SetActive(true);
        }

        private void OnPauseButtonClicked()
        {
            GameCycleService.Instance?.PauseGame();
            _pauseCanvas.gameObject.SetActive(true);
            _pauseButton.gameObject.SetActive(false);
        }

        private void OnRestartButtonClicked()
        {
            int currentScene = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentScene);
        }

        private void OnExitButtonClicked()
        {
            Application.Quit();
        }

        private void OnDisable()
        {
            _playButton.onClick.RemoveListener(OnPlayButtonClicked);
            _pauseButton.onClick.RemoveListener(OnPauseButtonClicked);
            _restartButton.onClick.RemoveListener(OnRestartButtonClicked);
            _exitButton.onClick.RemoveListener(OnExitButtonClicked);
        }

        private void OnDestroy()
        {
            if (_playerHealth != null)
            {
                _playerHealth.HealthChangedEvent -= OnHealthChanged;
            }
        }
    }
}