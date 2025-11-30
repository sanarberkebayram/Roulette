using System;

namespace Runtime.Zone
{
    [Serializable]
    public struct ZoneData
    {
        public int displayOrder;
        public ZoneType type;
        public string rewardId;
        public bool isClaimable;
        public bool hasBomb;
    }
}