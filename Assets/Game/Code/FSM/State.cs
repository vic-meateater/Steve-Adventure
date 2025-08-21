using System;
using System.Collections.Generic;

namespace SteveAdventure
{
    public abstract class State : IGamePauseListener, IGameResumeListener, IGameOverListener, IDisposable
    {
        private readonly List<Transition> _transitions = new();

        protected State()
        {
            GameCycleService.Instance?.AddListener(this);
        }
        
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
        public virtual void OnGamePause() { }
        public virtual void OnGameResume() { }
        public virtual void OnGameOver() { }

        public void Dispose()
        {
            GameCycleService.Instance?.RemoveListener(this);
        }
    }
}