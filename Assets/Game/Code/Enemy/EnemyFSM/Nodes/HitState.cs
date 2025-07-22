using UnityEngine;

namespace SteveAdventure
{
    //пока не используется, но может понадобиться в будущем
    public sealed class HitState : State
    {
        private readonly AnimationHandler _animationHandler;
        public HitState(AnimationHandler animationHandler)
        {
            _animationHandler = animationHandler;
        }
        
        public override void Enter()
        {
            Debug.Log("Enter to Hit State");
            _animationHandler.HitAnimationFrameStart();
        }
    }
}