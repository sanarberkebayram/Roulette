using System;
using Runtime.EventBus;

namespace Runtime.Inventory.Events
{
    [Serializable]
    public struct InventoryClearEvent : IEvent
    {
        public string inventoryId;
    }
}