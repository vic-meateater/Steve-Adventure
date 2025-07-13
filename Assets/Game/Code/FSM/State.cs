using System.Collections.Generic;

namespace SteveAdventure
{
    public abstract class State
    {
        private readonly List<Transition> _transitions = new();

        public void AddTransition(Transition transition)
        {
            _transitions.Add(transition);
        }

        public State GetNextState()
        {
            foreach (var transition in _transitions)
            {
                if (transition.Condition())
                    return transition.TargetState;
            }

            return null;
        }

        public virtual void Enter() { }
        public virtual void Exit() { }
        public virtual void Update() { }
    }
}