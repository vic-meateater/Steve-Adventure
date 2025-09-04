using UnityEngine;

namespace SteveAdventure
{
    public sealed class SettingsViewModel : ISettingsViewModel
    {
        public SettingsViewModel()
        {
        }

        public void OnMusicVolumeChanged(float value)
        {
            ChangeVolume("Music_Volume", value);
        }

        public void OnSoundVolumeChanged(float value)
        {
            ChangeVolume("Sound_Volume", value);
        }

        public void OnMusicToggleChanged(bool value)
        {
            SwitchMute("Music_Mute", value);
        }

        public void OnSoundToggleChanged(bool value)
        {
            SwitchMute("Sound_Mute", value);
        }

        private void SwitchMute(string key, bool value)
        {
            PlayerPrefs.SetInt(key, value ? 1 : 0);
        }

        private void ChangeVolume(string key, float value)
        {
            PlayerPrefs.SetFloat(key, value);
        }
    }
}