using System;

namespace SteveAdventure
{
    public sealed class Transition
    {
        public Func<bool> Condition { get; }
        public State TargetState { get; }

        public Transition(Func<bool> condition, State targetState)
        {
            Condition = condition ?? throw new ArgumentNullException(nameof(condition));
            TargetState = targetState ?? throw new ArgumentNullException(nameof(targetState));
        }
    }
}