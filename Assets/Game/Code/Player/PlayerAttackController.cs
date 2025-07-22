using System;
using System.Collections.Generic;
using UnityEngine;

namespace SteveAdventure
{
    public sealed class PlayerAttackController : IDisposable
    {
        private AnimationHandler _animationHandler;
        private readonly AnimatorController _animatorController;
        private readonly PlayerVision _playerVision;
        private readonly float _damage;

        public PlayerAttackController(AnimationHandler animationHandler, AnimatorController animatorController,
            PlayerVision playerVision, float damage)
        {
            _animationHandler = animationHandler;
            _animatorController = animatorController;
            _playerVision = playerVision;
            _damage = damage;

            _animationHandler.MeleeAttackStart += OnMeleeAttackStart;
            _animationHandler.AttackEnd += OnAttackEnd;
        }

        public void AttackRequest()
        {
            _animatorController.AttackAnimation();
        }

        private void OnMeleeAttackStart()
        {
            if (_playerVision.TryGetTargets(out List<GameObject> targets))
            {
                foreach (var target in targets)
                {
                    if(target.TryGetComponent(out IDamageable damageable))
                        damageable.TakeDamage(_damage);

                    if (target.TryGetComponent(out IAnimator animator))
                        animator.HitAnimation();
                }
            }
            else
            {
                Debug.LogWarning("No targets found for attack.");
            }
        }

        private void OnAttackEnd()
        {
        }

        public void Dispose()
        {
            if (_animationHandler)
            {
                _animationHandler.MeleeAttackStart -= OnMeleeAttackStart;
                _animationHandler.AttackEnd -= OnAttackEnd;
                _animationHandler = null;
            }
        }
    }
}