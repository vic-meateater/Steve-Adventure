using R3;

namespace SteveAdventure
{
    public sealed class GameOverViewModel : IGameOverViewModel
    {
        public string TitleText => "Game Over";
        public ReadOnlyReactiveProperty<bool> IsVisible { get; }

        public void PauseGame()
        {
            //throw new System.NotImplementedException();
        }

        public void ResumeGame()
        {
            //throw new System.NotImplementedException();
        }

        public void ExitGame()
        {
            //throw new System.NotImplementedException();
        }

        public void RestartGame()
        {
            //throw new System.NotImplementedException();
        }
    }
}