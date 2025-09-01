using UnityEngine;

namespace SteveAdventure
{
    public class PlayerSounds : IPlayerSounds
    {
        private readonly PlayerConfig _playerConfig;
        private readonly IAudioManager _audioManager;
        private float _nextFootstepTime;

        public PlayerSounds(PlayerConfig playerConfig, IAudioManager audioManager)
        {
            _playerConfig = playerConfig;
            _audioManager = audioManager;
        }
        
        public void PlayFootstepSound()
        {
            if (_playerConfig.FootstepSounds.Capacity == 0) return;
            int randomIndex = Random.Range(0, _playerConfig.FootstepSounds.Capacity);
            if (_nextFootstepTime < Time.time)
            {
                _nextFootstepTime = _playerConfig.FootstepSounds[randomIndex].length + Time.time;
                _audioManager.PlaySound(_playerConfig.FootstepSounds[randomIndex]);
            }
        }
        
        public void PlayDashSound()
        {
            _audioManager.PlaySound(_playerConfig.DashSound);
        }
        
        public void PlayHitSound()
        {
            _audioManager.PlaySound(_playerConfig.HitSound);
        }
        
        public void PlayPainSound()
        {
            _audioManager.PlaySound(_playerConfig.PainSound);
        }
    }

    public interface IPlayerSounds
    {
        void PlayFootstepSound();
        void PlayDashSound();
        void PlayHitSound();
        void PlayPainSound();
    }
}