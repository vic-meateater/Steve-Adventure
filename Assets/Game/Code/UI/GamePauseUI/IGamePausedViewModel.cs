using R3;

namespace SteveAdventure
{
    public interface IGamePausedViewModel : IViewModel
    {
        public string TitleText { get; }
        public ReadOnlyReactiveProperty<bool> IsVisible { get; }
        public void PauseGame();
        public void ResumeGame();
        public void ExitGame();
    }
}