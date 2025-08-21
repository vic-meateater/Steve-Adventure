using R3;
using UnityEngine.SceneManagement;

namespace SteveAdventure
{
    public sealed class GamePausedViewModel : IGamePausedViewModel
    {
        public string TitleText => "Game Paused";
        public ReadOnlyReactiveProperty<bool> IsVisible => _isVisible;
        private readonly ReactiveProperty<bool> _isVisible;

        public GamePausedViewModel()
        {
            _isVisible = new ReactiveProperty<bool>(false);
        }
        
        public void PauseGame()
        {
            GameCycleService.Instance?.PauseGame();
            _isVisible.Value = true;
        }

        public void ResumeGame()
        {
            GameCycleService.Instance?.ResumeGame();
            _isVisible.Value = false;
        }

        public void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void ExitGame()
        {
            SceneManager.LoadScene(sceneBuildIndex: 0);
        }
    }
}