using Zenject;

namespace SteveAdventure
{
    public sealed class SettingsPresenterFactory : IFactory<ISettingsViewModel>
    {
        public ISettingsViewModel Create()
        {
            return new SettingsViewModel();
        }
    }
}