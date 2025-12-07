using Runtime.Common;
using Runtime.Item;
using Runtime.Reward;
using Util;

namespace Runtime.Game
{
    public class InitializeState : IState
    {
        public StateType StateType => StateType.Initialize;
        private readonly IStateManager _stateManager;

        public InitializeState(IStateManager stateManager)
        {
            _stateManager = stateManager;
        }


        public void OnEnter()
        {
            _stateManager.ChangeState(StateType.Idle);
        }

        public void OnExit()
        {
        }

    }
}