using System;

namespace Runtime.Zone
{
    [Serializable]
    public struct ZoneData
    {
        public int displayOrder;
        public ZoneType type;
        public bool isClaimable;
        public bool hasBomb;
    }
}