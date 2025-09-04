namespace SteveAdventure
{
    public interface ISettingsViewModel
    {
        void OnMusicVolumeChanged(float value);
        void OnSoundVolumeChanged(float value);
        void OnMusicToggleChanged(bool value);
        void OnSoundToggleChanged(bool value);
    }
}