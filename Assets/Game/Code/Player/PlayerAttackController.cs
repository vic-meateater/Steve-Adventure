using System;
using UnityEngine;

namespace SteveAdventure
{
    public class PlayerAttackController : IDisposable
    {
        private AnimationHandler _animationHandler;
        private readonly AnimatorController _animatorController;

        public PlayerAttackController(AnimationHandler animationHandler, AnimatorController animatorController)
        {
            _animationHandler = animationHandler;
            _animatorController = animatorController;
            
            _animationHandler.OnStartAttackFrame += OnStartAttackAction;
            _animationHandler.OnEndAttackFrame += OnEndAttackAction;
        }

        public void AttackRequest()
        {
            _animatorController.AttackAnimation();
        }

        private void OnStartAttackAction()
        {
            Debug.Log("Start Attack Frame Triggered");
        }

        private void OnEndAttackAction()
        {
            Debug.Log("End Attack Frame Triggered");
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