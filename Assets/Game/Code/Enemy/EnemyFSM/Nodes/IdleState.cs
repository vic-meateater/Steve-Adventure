using UnityEngine;

namespace SteveAdventure
{
    public sealed class IdleState : State
    {
        private readonly float _waitDuration;
        private float _endTime;
        public IdleState(float waitDuration)
        {
            _waitDuration = waitDuration;
        }

        public override void Enter()
        {
            _endTime = Time.time + _waitDuration;
        }

        public bool IsTimeOver()
        {
            return Time.time >= _endTime;
        }
    }
}