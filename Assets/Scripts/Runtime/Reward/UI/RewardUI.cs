using System;
using System.Collections.Generic;
using Runtime.Common;
using Runtime.EventBus;
using Runtime.Inventory.Events;
using Runtime.Item;
using Runtime.ObjectPooler;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Runtime.Reward.UI
{
    public class RewardUI : MonoBehaviour
    {
        [SerializeField] private RectTransform slotParent;
        [SerializeField] private string inventoryId = "reward_inventory";
        
        [Inject] private PoolManager _poolManager;
        [Inject] private SceneEventBus _eventBus;
        [Inject] private IItemDatabase _itemDatabase;
        
        private readonly Dictionary<string, RewardUISlot> _slots = new();
        
        public Vector3 GetPosition(string itemId)
        {
            if (!_slots.TryGetValue(itemId, out var slot))
                throw new Exception($"Slot {itemId} not found");
            return slot.transform.position;
        }

        private void HandleInventoryClear(InventoryClearEvent obj)
        {
            if (_slots == null)
                return;
            
            foreach (var slot in _slots)
                Destroy(slot.Value.gameObject);
            
            _slots.Clear();
        }
        
        private void HandleInventoryChange(InventoryChangeEvent obj)
        {
            if (obj.inventoryId != inventoryId)
                return;

            if (!_slots.TryGetValue(obj.entry.itemId, out var slot))
            {
                _itemDatabase.TryGetItem(obj.entry.itemId, out var item);
                slot = _poolManager.Get<RewardUISlot>();
                slot.transform.SetParent(slotParent);
                slot.transform.localScale = Vector3.one;
                _slots.Add(obj.entry.itemId, slot);
                slot.SetItem(new SlotVisualData(){icon = item.sprite, slotText = "0"} );
                LayoutRebuilder.ForceRebuildLayoutImmediate(slotParent);
            }
            
            slot.UpdateCount(obj.entry.count);
        }
        
        private void OnEnable()
        {
            _eventBus.Subscribe<InventoryChangeEvent>(HandleInventoryChange);
            _eventBus.Subscribe<InventoryClearEvent>(HandleInventoryClear);
        }


        private void OnDisable()
        {
            _eventBus.Unsubscribe<InventoryChangeEvent>(HandleInventoryChange);
        }
    }
}