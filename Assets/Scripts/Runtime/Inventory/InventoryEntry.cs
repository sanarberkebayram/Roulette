using System;
using UnityEngine.Serialization;

namespace Runtime.Inventory
{
    [Serializable]
    public struct InventoryEntry
    {
        public string itemId;
        [FormerlySerializedAs("quantity")] public int count;
    }
}