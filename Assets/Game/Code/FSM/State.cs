using System.Collections.Generic;

namespace SteveAdventure
{
    public abstract class State
    {
        protected readonly BrainFSM Brain;
        private readonly List<Transition> _transitions = new();

        protected State(BrainFSM brain)
        {
            Brain = brain;
        }

        public virtual void Enter() { }

        public virtual void Update() { }
        
        public virtual void Exit() { }
        
        public void CheckTransitions()
        {
            foreach (var transition in _transitions)
            {
                if (transition.ShouldTransition())
                {
                    transition.OnTransition();
                    return;
                }
            }
        }
        
        public void AddTransition(Transition transition)
        {
            _transitions.Add(transition);
        }
    }
}