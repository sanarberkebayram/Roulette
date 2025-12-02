using System;

namespace Runtime.Reward
{
    [Serializable]
    public struct InventoryChange
    {
        public string uuid;
        public int oldAmount;
        public int newAmount;
    }
}