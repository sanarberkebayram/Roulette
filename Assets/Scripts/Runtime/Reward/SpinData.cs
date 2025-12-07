using System;

namespace Runtime.Reward
{
    [Serializable]
    public struct SpinData
    {
        public RewardData[] rewards;
        public int winnerIndex;
    }
}