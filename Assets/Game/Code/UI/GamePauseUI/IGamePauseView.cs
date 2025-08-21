using R3;

namespace SteveAdventure
{
    public interface IGamePauseView
    { 
        void Init(IGamePausedViewModel viewModel);
        void Show();
    }
}