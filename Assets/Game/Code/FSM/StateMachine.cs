using System;
using System.Collections.Generic;
using UnityEngine;

namespace SteveAdventure
{
    abstract class StateMachine
    {
        protected State CurrentState;
        protected Dictionary<Type, State> States = new();

        public void Update()
        {
            if (CurrentState == null) return;

            CurrentState.Update();
            CurrentState.TryTransition();
        }

        public void ChangeState<TState>() where TState : State
        {
            if (CurrentState != null && CurrentState.GetType() == typeof(TState))
                return;

            if (States.TryGetValue(typeof(TState), out State newState))
            {
                CurrentState.Exit();
                CurrentState = newState;
                CurrentState.Enter();
            }
            else
            {
                Debug.LogError($"State {typeof(TState)} not found");
            }
        }
    }

    abstract class State
    {
        protected Transition[] Transitions;

        protected State(StateMachine stateMachine)
        {
        }

        public virtual void Enter()
        {
        }

        public virtual void Exit()
        {
        }

        public virtual void Update()
        {
        }

        public virtual void TryTransition()
        {
            foreach (var transition in Transitions)
            {
                if (transition.IsNeedTransit())
                {
                    transition.Transit();
                    return;
                }
            }
        }
    }

    abstract class Transition
    {
        protected StateMachine StateMachine;

        protected Transition(StateMachine stateMachine)
        {
            StateMachine = stateMachine;
        }

        public abstract bool IsNeedTransit();
        public abstract void Transit();
    }

    class EnemyStateMachine : StateMachine
    {
    }

    class PatrolState : State
    {
        private readonly Mover _mover;
        private readonly Transform[] _waypoints;
        private int _currentWaypointIndex;
        private readonly Collider2D _collider;

        public PatrolState(StateMachine stateMachine, Mover mover, Transform[] waypoints, Collider2D collider) : base(stateMachine)
        {
            _collider = collider;
            _waypoints = waypoints;
            _mover = mover;
        }

        public override void Update()
        {
            Transform currentWaypoint = _waypoints[_currentWaypointIndex];
            Vector2 colliderCenter = _collider.bounds.center;
            Vector2 direction = (currentWaypoint.position - (Vector3)colliderCenter).normalized;
            _mover.Moving(direction);
        }
    }

    class IdleState : State
    {
        public bool IsEndWaiting => _endWaitingTime <= Time.time; 
        
        private readonly float _waitTime;
        private float _endWaitingTime;

        public IdleState(StateMachine stateMachine, float waitTime) : base(stateMachine)
        {
            _waitTime = waitTime;
            
            Transitions = new[]
            {
                new EndIdleTransition(stateMachine, this)
            };
        }

        public override void Enter()
        {
            _endWaitingTime = Time.time + _waitTime;
        }
    }

    //class FollowState : State { }

    //class DetectTargetTransition : Transition { }

    //class LostTargetTransition : Transition { }

    class EndIdleTransition : Transition
    {
        private readonly IdleState _idleState;

        public EndIdleTransition(StateMachine stateMachine, IdleState idleState) : base(stateMachine)
        {
            _idleState = idleState;
        }

        public override bool IsNeedTransit()
        {
            return _idleState.IsEndWaiting;
        }

        public override void Transit()
        {
            StateMachine.ChangeState<PatrolState>();
        }
    }

    class TargetReachedTransition : Transition
    {
        private readonly WaypointsMoveController _wayPointsController;

        public TargetReachedTransition(
            StateMachine stateMachine, 
            WaypointsMoveController wayPointsController) 
            : base(stateMachine)
        {
            _wayPointsController = wayPointsController;
        }

        public override bool IsNeedTransit()
        {
            return _wayPointsController.IsWayPointReached;
        }

        public override void Transit()
        {
            StateMachine.ChangeState<IdleState>();
        }
    }
}