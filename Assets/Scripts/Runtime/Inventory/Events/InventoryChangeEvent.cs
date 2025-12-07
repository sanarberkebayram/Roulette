using System;
using Runtime.EventBus;
using UnityEngine.Serialization;

namespace Runtime.Inventory.Events
{
    [Serializable]
    public struct InventoryChangeEvent : IEvent
    {
        public string inventoryId;
        [FormerlySerializedAs("inventoryEntry")] public InventoryEntry entry;
    }
}