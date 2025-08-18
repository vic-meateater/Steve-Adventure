using R3;

namespace SteveAdventure
{
    public interface IGameOverViewModel : IViewModel
    {
        public string TitleText { get; }
        public ReadOnlyReactiveProperty<bool> IsVisible { get; }
        public void RestartGame();
        public void ExitGame();
        public void GameOver();
    }
}