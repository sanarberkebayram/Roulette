using Runtime.Common;
using Runtime.EventBus;
using Runtime.Item;
using Runtime.Player.Choice;
using Runtime.Reward;
using Util;

namespace Runtime.Game
{
    public class IdleState: IState
    {
        public StateType StateType => StateType.Idle;
        private readonly SceneEventBus _eventBus;
        private readonly IStateManager _stateManager;
        private readonly RewardController _rewardController;
        private readonly WheelController _wheelController;
        private readonly IItemDatabase _itemDatabase;
        

        public IdleState(SceneEventBus eventBus, IStateManager stateManager, RewardController rewardController, WheelController wheelController, IItemDatabase itemDatabase)
        {
            _eventBus = eventBus;
            _stateManager = stateManager;
            _rewardController = rewardController;
            _wheelController = wheelController;
            _itemDatabase = itemDatabase;

            _eventBus.Subscribe<SpinRequestEvent>(HandleSpinRequest);
            _eventBus.Subscribe<ClaimRequestEvent>(HandleClaimRequest);
        }



        public void OnEnter()
        {
            var spinData = _rewardController.GetSpinData();
            _wheelController.SetSpinInfo(spinData.ConvertVisuals(_itemDatabase));
        }

        public void OnExit()
        {
        }

        private void HandleSpinRequest(SpinRequestEvent evt)
        {
            _stateManager.ChangeState(StateType.Spin);
        }
        
        private void HandleClaimRequest(ClaimRequestEvent obj)
        {
            _stateManager.ChangeState(StateType.Claim);
        }
    }
}