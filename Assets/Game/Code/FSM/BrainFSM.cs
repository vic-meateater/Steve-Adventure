using System.Collections.Generic;

namespace SteveAdventure
{
    public abstract class BrainFSM
    {
        private State _currentState;
        private readonly List<State> _allStates = new();

        public virtual void Update()
        {
            _currentState?.Update();
            var next = _currentState?.GetNextState();
            if (next != null)
                ChangeState(next);
        }

        protected void SetInitialState(State initial)
        {
            _currentState = initial;
            _currentState.Enter();
        }

        protected void AddState(State state)
        {
            if (!_allStates.Contains(state))
                _allStates.Add(state);
        }

        private void ChangeState(State newState)
        {
            if (_currentState == newState)
                return;

            _currentState?.Exit();
            _currentState = newState;
            _currentState.Enter();
        }
    }
}