using System;

namespace Runtime.Reward
{
    [Serializable]
    public struct RewardData
    {
        public string uuid;
        public int count;
        public bool isBomb;
    }
}