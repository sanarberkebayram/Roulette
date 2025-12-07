using Runtime.EventBus;
using Runtime.Inventory;
using Runtime.Reward;
using Runtime.Reward.UI;
using Runtime.Zone;

namespace Runtime.Game
{
    public class RewardState : IState
    {
        public StateType StateType => StateType.Reward;
        private readonly RewardAnimator _animator;
        private readonly RewardController _rewardController;
        private readonly IInventory _inventory;
        private readonly IStateManager _stateManager;
        private readonly ZoneController _zoneController;

        public RewardState(RewardAnimator animator, RewardController rewardController, IInventory inventory, SceneEventBus eventBus, IStateManager stateManager, ZoneController zoneController)
        {
            _animator = animator;
            _rewardController = rewardController;
            _inventory = inventory;
            _stateManager = stateManager;
            _zoneController = zoneController;

            eventBus.Subscribe<RewardAnimFinishEvent>(HandleAnimFinish);
        }

        public void OnEnter()
        {
            var currSpin = _rewardController.CurrentSpin;
            var winner = currSpin.rewards[currSpin.winnerIndex];
             _inventory.Add(new InventoryEntry()
             {
                 itemId = winner.uuid,
                 count = 0
             });
             
            _animator.Animate(winner.uuid, currSpin.winnerIndex);
        }

        public void OnExit()
        {
        }
        
        private void HandleAnimFinish(RewardAnimFinishEvent obj)
        {
            var currSpin = _rewardController.CurrentSpin;
            var winner = currSpin.rewards[currSpin.winnerIndex];
            _inventory.Add(new InventoryEntry()
            {
                itemId = winner.uuid,
                count = winner.count
            });
            
            DecideNextState();
        }

        private void DecideNextState()
        {
            if (_zoneController.CanIncreaseZone())
            {
                _zoneController.IncreaseZone();
                _stateManager.ChangeState(StateType.Idle);
            }
            else
            {
                _stateManager.ChangeState(StateType.Claim);
            }
        }
    }
}
