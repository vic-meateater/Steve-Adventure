using Zenject;

namespace SteveAdventure
{
    public sealed class SettingsPresenterFactory : IFactory<IAudioManager, ISettingsViewModel>
    {
        public ISettingsViewModel Create(IAudioManager audioManager)
        {
            return new SettingsViewModel(audioManager);
        }
    }
}