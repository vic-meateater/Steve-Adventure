using UnityEngine;
using Zenject;

namespace SteveAdventure
{
    public sealed class SettingsViewModel : ISettingsViewModel
    {
        private const string MUSIC_VOLUME_KEY = "Music_Volume";
        private const string SOUND_VOLUME_KEY = "Sound_Volume";
        private const string MUSIC_MUTE_KEY = "Music_Mute";
        private const string SOUND_MUTE_KEY = "Sound_Mute";
        
        private IAudioManager _audioManager;

        public SettingsViewModel(IAudioManager audioManager)
        {
            _audioManager = audioManager;
            _audioManager.RefreshSettings();
        }

        public void OnMusicVolumeChanged(float value)
        {
            ChangeVolume(MUSIC_VOLUME_KEY, value);
        }

        public void OnSoundVolumeChanged(float value)
        {
            ChangeVolume(SOUND_VOLUME_KEY, value);
        }

        public void OnMusicToggleChanged(bool value)
        {
            SwitchMute(MUSIC_MUTE_KEY, value);
        }

        public void OnSoundToggleChanged(bool value)
        {
            SwitchMute(SOUND_MUTE_KEY, value);
        }
        
        public float GetMusicVolume()
        {
            return PlayerPrefs.GetFloat(MUSIC_VOLUME_KEY, .1f);
        }
        public float GetSoundVolume()
        {
            return PlayerPrefs.GetFloat(SOUND_VOLUME_KEY, .1f);
        }

        public bool IsMusicMuted()
        {
            return PlayerPrefs.GetInt(MUSIC_MUTE_KEY) == 1;
        }
        
        public bool IsSoundMuted()
        {
            return PlayerPrefs.GetInt(SOUND_MUTE_KEY) == 1;
        }

        private void SwitchMute(string key, bool value)
        {
            PlayerPrefs.SetInt(key, value ? 1 : 0);
            _audioManager.RefreshSettings();
        }

        private void ChangeVolume(string key, float value)
        {
            PlayerPrefs.SetFloat(key, value);
            _audioManager.RefreshSettings();
        }
    }
}