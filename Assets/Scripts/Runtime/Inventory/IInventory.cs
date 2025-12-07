using System.Collections.Generic;

namespace Runtime.Inventory
{
    public interface IInventory
    {
        string Id { get; }
        void Add(InventoryEntry entry);
        void Clear();
        void Transfer(IInventory other);
        InventoryEntry[] GetStatus();
    }
}