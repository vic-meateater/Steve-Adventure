using R3;

namespace SteveAdventure
{
    public interface IGamePauseView
    {
        public ReadOnlyReactiveProperty<bool> IsVisible { get; }
        void Init(IGamePausedViewModel viewModel);
        void Show();
    }
}