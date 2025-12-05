using System;

namespace Runtime.Reward
{
    [Serializable]
    public struct SpinInfo
    {
        public RewardInfo[] rewards;
        public int winnerIndex;
        public bool bombExploded;
    }
}