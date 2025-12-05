using System;
using Runtime.WheelItem;
using UnityEngine;

namespace Runtime.Reward
{
    [Serializable]
    public struct RewardCountData
    {
        public int zoneOrder;
        public Vector2Int regularItemCountInterval;
        public Vector2Int silverItemCountInterval;
        public Vector2Int goldItemCountInterval;
    }
}