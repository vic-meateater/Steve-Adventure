using UnityEngine;

namespace SteveAdventure
{
    public class AudioManager : IAudioManager
    {
        private AudioSource _musicSource;
        private AudioSource _soundSource;
        private AudioClip _defaultMusic;

        public AudioManager(AudioSource musicSource, AudioSource soundSource, AudioClip defaultMusic)
        {
            _musicSource = musicSource;
            _soundSource = soundSource;
            _defaultMusic = defaultMusic;
            PlayDefaultMusic();
            RefreshSettings();
        }
        
        public void PlayMusic(AudioClip clip)
        {
            _musicSource.Stop();
            _musicSource.clip = clip;
            _musicSource.Play();
        }
        
        public void PlaySound(AudioClip clip)
        {
            _soundSource.PlayOneShot(clip);
        }
        
        public void RefreshSettings()
        {
            float musicVolume = PlayerPrefs.GetFloat("Music_Volume", 1f);
            float soundVolume = PlayerPrefs.GetFloat("Sound_Volume", 1f);
            bool isMusicMuted = PlayerPrefs.GetInt("Music_Mute", 0) == 1;
            bool isSoundMuted = PlayerPrefs.GetInt("Sound_Mute", 0) == 1;

            _musicSource.volume = isMusicMuted ? 0f : musicVolume;
            _soundSource.volume = isSoundMuted ? 0f : soundVolume;
        }
        
        private void PlayDefaultMusic()
        {
            if (_defaultMusic != null)
            {
                PlayMusic(_defaultMusic);
            }
        }
    }
}