using System;
using System.Collections.Generic;
using Enums;
using UnityEngine;
using Logger = Core.Logger;

namespace Base
{
    public abstract class StateMachine
    {
        private Dictionary<Type, StateMachine> _subStates = new();
        private Dictionary<int, StateMachine> _transitions = new();

        private StateMachine _parent;
        private StateMachine _defaultSubState;
        private StateMachine _currentSubState;

        public void Enter()
        {
            OnEnter();

            if (_currentSubState is null && _defaultSubState is not null)
            {
                _currentSubState = _defaultSubState;
            }

            _currentSubState?.Enter();
        }

        public void Update()
        {
            OnUpdate();
            _currentSubState?.Update();
        }

        public void Exit()
        {
            _currentSubState?.Exit();
            if (_defaultSubState is not null) _currentSubState = _defaultSubState;
            
            OnExit();
        }

        public void AddTransition(StateMachine sourceStateMachine, StateMachine targetStateMachine,
            int trigger)
        {
            if (sourceStateMachine._transitions.ContainsKey(trigger))
            {
                Debug.LogWarning("Duplicated transition! : " + trigger);
                return;
            }

            sourceStateMachine._transitions.Add(trigger, targetStateMachine);
        }

        public void AddSubState(StateMachine subStateMachine)
        {
            if (_subStates.Count is 0)
            {
                _defaultSubState = subStateMachine;
            }

            subStateMachine._parent = this;

            if (_subStates.ContainsKey(subStateMachine.GetType()))
            {
                Debug.LogWarning("Duplicated sub state : " + subStateMachine.GetType());
                return;
            }

            _subStates.Add(subStateMachine.GetType(), subStateMachine);
        }

        public void SendTrigger(int trigger)
        {
            var root = GetRootStateMachine();

            while (root is not null)
            {
                if (root._transitions.TryGetValue(trigger, out StateMachine toState))
                {
                    root._parent?.ChangeSubState(toState);
                    return;
                }

                root = root._currentSubState;
            }
        }

        private StateMachine GetRootStateMachine()
        {
            var root = this;
            while (root?._parent is not null)
            {
                root = root._parent;
            }

            return root;
        }

        private void ChangeSubState(StateMachine stateMachine)
        {
            _currentSubState?.Exit();
            var nextState = _subStates[stateMachine.GetType()];
            _currentSubState = nextState;
            nextState.Enter();
        }

        protected virtual void OnEnter()
        {
        }

        protected virtual void OnUpdate()
        {
        }

        protected virtual void OnExit()
        {
        }
    }
}