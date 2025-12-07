using Runtime.EventBus;
using Runtime.Player.Choice;
using Runtime.Reward.Strategy;
using Runtime.Zone;

namespace Runtime.Reward
{
    public class RewardController
    {
        private readonly IRewardStrategy _strategy;
        private readonly float _initialMultiplier;
        
        private float _rewardMultiplier;
        public SpinData CurrentSpin { get; private set; }
        
        public RewardController(IRewardStrategy strategy, float rewardMultiplier, ZoneController zoneController, SceneEventBus eventBus)
        {
            _strategy = strategy;
            _rewardMultiplier = rewardMultiplier;
            _initialMultiplier = rewardMultiplier;

            _rewardMultiplier *= zoneController.CurrentZone.rewardMultiplier;
            eventBus.Subscribe<ZoneChangeEvent>(HandleZoneChange);
            eventBus.Subscribe<RestartEvent>(HandleRestart);
        }


        public SpinData GetSpinData()
        {
            var data = _strategy.GetSpinData();
            ApplyMultiplier(ref data);
            CurrentSpin = data;
            return data;
        }
        
        private void HandleRestart(RestartEvent obj)
        {
            _rewardMultiplier = _initialMultiplier;
        }
        
        private void HandleZoneChange(ZoneChangeEvent obj)
        {
            _rewardMultiplier *= obj.newZone.rewardMultiplier;
        }

        private void ApplyMultiplier(ref SpinData data)
        {
            for (var i = 0; i < data.rewards.Length; i++)
            {
                if (data.rewards[i].isBomb)
                    continue;

                data.rewards[i].count = (int)(_rewardMultiplier * data.rewards[i].count);
            }
        }
    }
}