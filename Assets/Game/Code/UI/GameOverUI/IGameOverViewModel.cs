using R3;

namespace SteveAdventure
{
    public interface IGameOverViewModel : IViewModel
    {
        public string TitleText { get; }
        public ReadOnlyReactiveProperty<bool> IsVisible { get; }
        public void PauseGame();
        public void ExitGame();
        void RestartGame();
    }
}