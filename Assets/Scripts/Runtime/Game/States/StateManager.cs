using System;
using System.Collections.Generic;
using Runtime.EventBus;
using UnityEngine;

namespace Runtime.Game
{
    public class StateManager : IStateManager
    {
        private readonly SceneEventBus _eventBus;
        private IState _current;
        private Dictionary<StateType, IState> _states = new();

        public StateManager(SceneEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public StateType CurrentState => _current?.StateType ?? StateType.Unset;
        
        public void ChangeState(StateType newState)
        {
            if (!_states.ContainsKey(newState))
                throw new Exception($"State({newState}) not found");
            
            var oldState = _current?.StateType ?? StateType.Unset;
            
            _current?.OnExit();
            _current = _states[newState];
            _current.OnEnter();
            
            _eventBus.Raise(new StateChangeEvent(){oldState = oldState, newState = newState});
        }

        public void AddState(IState state) => _states.Add(state.StateType, state); 
    }
}