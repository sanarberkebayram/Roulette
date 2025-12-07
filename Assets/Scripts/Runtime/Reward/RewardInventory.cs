using System.Collections.Generic;
using Runtime.EventBus;
using Runtime.Inventory;
using Runtime.Inventory.Events;
using Runtime.Player.Choice;
using Runtime.Reward.UI;

namespace Runtime.Reward
{
    public class RewardInventory : IInventory
    {
        public string Id => _id;
        private readonly Dictionary<string, int> _data;
        private readonly string _id;
        private readonly SceneEventBus _eventBus;
        private readonly RewardUI _rewardUI;

        public RewardInventory(string id,  SceneEventBus eventBus, RewardUI rewardUI)
        {
            _id = id;
            _eventBus = eventBus;
            _rewardUI = rewardUI;
            _data = new Dictionary<string, int>();
            
            _eventBus.Subscribe<RestartEvent>(HandleRestart);
        }
        


        public void Add(InventoryEntry entry)
        {
            if (_data.ContainsKey(entry.itemId))
                _data[entry.itemId] += entry.count;
            else 
                _data.Add(entry.itemId, entry.count);
            
            _eventBus.Raise(new InventoryChangeEvent(){inventoryId = Id, entry = new InventoryEntry(){itemId = entry.itemId, count = _data[entry.itemId]}});
        }

        public void Clear()
        {
            _data.Clear();
            _eventBus.Raise(new InventoryClearEvent(){inventoryId = Id});
        }

        public void Transfer(IInventory other)
        {
            foreach (var data in _data)
            {
                var transferData = new InventoryEntry()
                {
                    itemId = data.Key,
                    count = data.Value
                };
                other.Add(transferData);
            }
            Clear();
        }

        public InventoryEntry[] GetStatus()
        {
            var result = new InventoryEntry[_data.Count];
            
            var i = 0;
            foreach (var data in _data)
            {
                result[i++] = new InventoryEntry()
                {
                    itemId = data.Key,
                    count = data.Value
                };
            }
            return result;
        }

        void HandleRestart(RestartEvent evt)
        {
            Clear();
        }
    }
}