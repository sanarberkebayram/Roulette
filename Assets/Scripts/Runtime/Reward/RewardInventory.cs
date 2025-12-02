using System;
using System.Collections.Generic;

namespace Runtime.Reward
{
    public class RewardInventory
    {
        public Action<InventoryChange> OnInventoryChange;
        public Action OnInventoryClear;
        
        private readonly Dictionary<string, int> _rewards = new Dictionary<string, int>();
        
        public void AddReward(RewardInfo rewardInfo)
        {
            if (_rewards.TryGetValue(rewardInfo.uuid, out var oldAmount))
            {
                var newAmount = oldAmount + rewardInfo.amount;
                _rewards[rewardInfo.uuid] = newAmount;
                OnInventoryChange?.Invoke(
                    new InventoryChange
                    {
                        uuid = rewardInfo.uuid,
                        oldAmount = oldAmount,
                        newAmount = newAmount
                    }
                );
                return;
            }
            _rewards.Add(rewardInfo.uuid, rewardInfo.amount);
            OnInventoryChange?.Invoke(
                new InventoryChange
                {
                    uuid = rewardInfo.uuid,
                    oldAmount = 0,
                    newAmount = rewardInfo.amount
                });
        }
        
        public void Clear()
        {
            _rewards.Clear();
            OnInventoryClear?.Invoke();
        }
    }
}