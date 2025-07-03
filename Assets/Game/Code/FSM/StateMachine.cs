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
                CurrentState?.Exit();
                CurrentState = newState;
                CurrentState.Enter();
            }
            else
            {
                Debug.LogError($"State {typeof(TState)} not found");
            }
        }
        
        public State GetCurrentState() => CurrentState;
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
        public EnemyStateMachine(Mover mover, Transform[] waypoints, Collider2D collider)
        {
            States = new Dictionary<Type, State>()
            {
                { typeof(PatrolState), new PatrolState(this, mover, waypoints, collider) },
                { typeof(IdleState), new IdleState(this, 1f) }
            };

            ChangeState<PatrolState>();
        }
    }

    class PatrolState : State
    {
        private const float WAYPOINT_OFFSET = 0.5f;
        private const int WAYPOINT_STEP = 1;

        private readonly Mover _mover;
        private readonly Transform[] _waypoints;
        private int _currentWaypointIndex;
        private readonly Collider2D _collider;

        public PatrolState(StateMachine stateMachine, Mover mover, Transform[] waypoints, Collider2D collider) : base(
            stateMachine)
        {
            _collider = collider;
            _waypoints = waypoints;
            _mover = mover;

            Transitions = new[]
            {
                new TransitionToIdle(stateMachine, this)
            };
        }


        public override void Enter()
        {
            _currentWaypointIndex = (_currentWaypointIndex + WAYPOINT_STEP) % _waypoints.Length;
        }

        public override void Update()
        {
            Transform currentWaypoint = _waypoints[_currentWaypointIndex];
            Vector2 colliderCenter = _collider.bounds.center;
            Vector2 direction = (currentWaypoint.position - (Vector3)colliderCenter).normalized;
            _mover.Moving(direction);
        }

        public bool IsWaypointReached()
        {
            float sqrDistance = (_collider.bounds.center - _waypoints[_currentWaypointIndex].position).sqrMagnitude;
            return sqrDistance < WAYPOINT_OFFSET * WAYPOINT_OFFSET;
        }

        public void MoveToNextWaypoint()
        {
            _currentWaypointIndex = (_currentWaypointIndex + 1) % _waypoints.Length;
        }
    }

    class IdleState : State
    {
        public bool IsEndWaiting => _endWaitingTime <= Time.time;

        private readonly float _waitTime = 2f;
        private float _endWaitingTime;

        public IdleState(StateMachine stateMachine, float waitTime) : base(stateMachine)
        {
            _waitTime = waitTime;

            Transitions = new[]
            {
                new TransitionToPatrol(stateMachine)
            };
        }

        public override void Enter()
        {
            _endWaitingTime = Time.time + 2f;
        }
        
        public bool IsWaitOver()
        {
            return Time.time >= _endWaitingTime;
        }
    }
    
    class Transitions : Transition
    {
        private readonly Func<bool> _condition;
        private readonly Action _onTransit;

        public Transitions(StateMachine stateMachine, Func<bool> condition, Action onTransit)
            : base(stateMachine)
        {
            _condition = condition;
            _onTransit = onTransit;
        }

        public override bool IsNeedTransit() => _condition();
        public override void Transit() => _onTransit();
    }
    
    class TransitionToIdle : Transition
    {
        private readonly PatrolState _patrolState;

        public TransitionToIdle(StateMachine stateMachine, PatrolState patrolState) : base(stateMachine)
        {
            _patrolState = patrolState;
        }

        public override bool IsNeedTransit()
        {
            return _patrolState.IsWaypointReached();
        }

        public override void Transit()
        {
            _patrolState.MoveToNextWaypoint();
            StateMachine.ChangeState<IdleState>();
        }
    }
    
    class TransitionToPatrol : Transition
    {
        public TransitionToPatrol(StateMachine stateMachine) : base(stateMachine)
        {
        }

        public override bool IsNeedTransit()
        {
            if (StateMachine is EnemyStateMachine enemyStateMachine &&
                enemyStateMachine.GetCurrentState() is IdleState idleState)
            {
                return idleState.IsWaitOver();
            }

            return false;
        }

        public override void Transit()
        {
            StateMachine.ChangeState<PatrolState>();
        }
    }



    //class FollowState : State { }

    //class DetectTargetTransition : Transition { }

    //class LostTargetTransition : Transition { }

    // class EndIdleTransition : Transition
    // {
    //     private readonly IdleState _idleState;
    //
    //     public EndIdleTransition(StateMachine stateMachine, IdleState idleState) : base(stateMachine)
    //     {
    //         _idleState = idleState;
    //     }
    //
    //     public override bool IsNeedTransit()
    //     {
    //         return _idleState.IsEndWaiting;
    //     }
    //
    //     public override void Transit()
    //     {
    //         StateMachine.ChangeState<PatrolState>();
    //     }
    // }

    //
    // class TargetReachedTransition : Transition
    // {
    //     private readonly WaypointsMoveController _wayPointsController;
    //
    //     public TargetReachedTransition(
    //         StateMachine stateMachine, WaypointsMoveController wayPointsController)
    //         : base(stateMachine)
    //     {
    //         _wayPointsController = wayPointsController;
    //     }
    //
    //     public override bool IsNeedTransit()
    //     {
    //         return _wayPointsController.IsWayPointReached;
    //     }
    //
    //     public override void Transit()
    //     {
    //         StateMachine.ChangeState<IdleState>();
    //     }
    // }

}