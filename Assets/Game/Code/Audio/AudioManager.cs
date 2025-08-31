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
        
        private void PlayDefaultMusic()
        {
            if (_defaultMusic != null)
            {
                PlayMusic(_defaultMusic);
            }
        }
    }
}