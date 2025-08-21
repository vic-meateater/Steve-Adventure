using R3;
using UnityEngine.SceneManagement;

namespace SteveAdventure
{
    public sealed class GameOverViewModel : IGameOverViewModel
    {
        public string TitleText => "Game Over";
        public ReadOnlyReactiveProperty<bool> IsVisible => _isVisible;
        private readonly ReactiveProperty<bool> _isVisible;

        public GameOverViewModel()
        {
            _isVisible = new ReactiveProperty<bool>(false);
        }
        
        public void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void ExitGame()
        {
            SceneManager.LoadScene(sceneBuildIndex: 0);
        }

        public void GameOver()
        {
            _isVisible.Value = true;
        }
    }
}