using System;
using System.Collections.Generic;
using UnityEngine;

namespace SteveAdventure
{
    public abstract class BrainFSM
    {
        private State _currentState;
        private Dictionary<Type, State> _states = new();
       
        public void Update()
        {
            _currentState?.Update();
            _currentState?.CheckTransitions();
        }
        
        public void ChangeState<TState>() where TState : State
        {
            Type stateType = typeof(TState);

            if (!_states.ContainsKey(stateType))
            {
                Debug.LogError($"State {stateType} не зарегистрировано в StateMachine");
                return;
            }

            if (_currentState?.GetType() == stateType)
                return;

            _currentState?.Exit();
            _currentState = _states[stateType];
            _currentState.Enter();
        }
        
        public void RegisterState(State state)
        {
            Type type = state.GetType();
            if (_states.ContainsKey(type))
            {
                Debug.LogWarning($"Состояние {type} уже зарегистрировано");
                return;
            }

            _states.Add(type, state);
        }

        public State GetCurrentState() => _currentState;
    }
}