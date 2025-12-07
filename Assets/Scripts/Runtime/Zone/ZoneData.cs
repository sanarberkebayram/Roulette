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
        public float rewardMultiplier;

        public static ZoneData Default(int order)
        {
            return new ZoneData()
            {
                displayOrder = order,
                type = ZoneType.Regular,
                isClaimable = false,
                hasBomb = true,
                rewardMultiplier = 1.15f,
            };
        }
    }
}