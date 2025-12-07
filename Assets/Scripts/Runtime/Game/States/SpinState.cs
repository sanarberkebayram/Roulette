
using Runtime.Common;
using Runtime.EventBus;
using Runtime.Reward;
using Runtime.Wheel;

namespace Runtime.Game
{
    public class SpinState : IState
    {
        public StateType StateType => StateType.Spin;
        private readonly SceneEventBus _eventBus;
        private readonly WheelController _wheelController;
        private readonly RewardController _rewardController;
        private readonly IStateManager _stateManager;

        public SpinState(
            SceneEventBus eventBus,
            WheelController wheelController,
            RewardController rewardController, IStateManager stateManager)
        {
            _eventBus = eventBus;
            _wheelController = wheelController;
            _rewardController = rewardController;
            _stateManager = stateManager;

            _eventBus.Subscribe<SpinFinishEvent>(HandleSpinEnd);
        }

        private void HandleSpinEnd(SpinFinishEvent obj)
        {
            var spinData = _rewardController.CurrentSpin;
            var reward = spinData.rewards[spinData.winnerIndex];
            _stateManager.ChangeState(reward.isBomb ? StateType.Bomb : StateType.Reward);
        }

        public void OnEnter()
        {
            _wheelController.Spin();
        }

        public void OnExit()
        {
        }
    }
}