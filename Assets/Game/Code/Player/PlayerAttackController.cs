using System;
using UnityEngine;

namespace SteveAdventure
{
    public class PlayerAttackController : IDisposable
    {
        private AnimationHandler _animationHandler;
        private readonly AnimatorController _animatorController;
        private readonly PlayerVision _playerVision;

        public PlayerAttackController(AnimationHandler animationHandler, AnimatorController animatorController,
            PlayerVision playerVision)
        {
            _animationHandler = animationHandler;
            _animatorController = animatorController;
            _playerVision = playerVision;

            _animationHandler.OnStartAttackFrame += OnStartAttackAction;
            _animationHandler.OnEndAttackFrame += OnEndAttackAction;
        }

        public void AttackRequest()
        {
            _animatorController.AttackAnimation();
        }

        private void OnStartAttackAction()
        {
            if (_playerVision.TryGetTargets(out var targets))
            {
                foreach (var target in targets)
                {
                    Debug.LogWarning($"Target {target.name} damaged by player attack.");
                }
            }
            else
            {
                Debug.LogWarning("No targets found for attack.");
            }
        }

        private void OnEndAttackAction()
        {
        }

        public void Dispose()
        {
            if (_animationHandler != null)
            {
                _animationHandler.OnStartAttackFrame -= OnStartAttackAction;
                _animationHandler.OnEndAttackFrame -= OnEndAttackAction;
                _animationHandler = null;
            }
        }
    }
}