using R3;

namespace SteveAdventure
{
    public interface IGameViewModel : IViewModel
    {
        public IGamePausedViewModel GamePausedViewModel { get; }
        public IGameOverViewModel GameOverViewModel { get; }
        public ISettingsViewModel SettingsViewModel { get; }
    }
}