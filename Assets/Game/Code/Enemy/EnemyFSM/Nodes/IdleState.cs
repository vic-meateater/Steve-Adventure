using UnityEngine;

namespace SteveAdventure
{
    public class IdleState : State
    {
        private readonly float _waitDuration;
        private float _endTime;
        public IdleState(EnemyBrain brain, float waitDuration) : base(brain)
        {
            _waitDuration = waitDuration;
        }

        public override void Enter()
        {
            Debug.Log("Enter to Idle State");
            _endTime = Time.time + _waitDuration;
        }
        
        public bool IsTimeOver()
        {
            return Time.time >= _endTime;
        }
    }
}