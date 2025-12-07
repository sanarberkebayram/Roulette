using System;
using Runtime.EventBus;

namespace Runtime.Game
{
    public interface IStateManager
    {
        StateType CurrentState { get; }
        void ChangeState(StateType newState);
        void AddState(IState state);
    }

    [Serializable]
    public struct StateChangeEvent : IEvent
    {
        public StateType oldState;
        public StateType newState;
    }
}