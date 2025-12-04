using System.Collections.Generic;
using Runtime.WheelItem;
using UnityEngine;
using Zenject;

namespace Runtime.Reward.UI
{
    public class RewardUI : MonoBehaviour
    {
        [SerializeField] private RewardUISlot slotPrefab;
        [SerializeField] private Transform slotParent;
        
        [Inject] private WheelItemDatabase _itemDatabase;
        [Inject] private RewardInventory _inventory;

        private readonly Dictionary<string, RewardUISlot> _slots = new();
        
        // RewardInfo is small, so no need to get with ref
        public void DisplayReward(RewardInfo info)
        {
            if (_slots.ContainsKey(info.uuid))
            {
                var slot = _slots[info.uuid];
                return;
            }
            
            InitializeNewReward(ref info);
        }
        
        private void InitializeNewReward(ref RewardInfo info)
        {
            if (_slots.ContainsKey(info.uuid)) 
                return;

            var wheelItem = _itemDatabase.GetItem(info.uuid);
            var slot = Instantiate(slotPrefab, slotParent);
            slot.SetItem(wheelItem.sprite, 0);
            _slots.Add(info.uuid, slot);
        }
    }
}