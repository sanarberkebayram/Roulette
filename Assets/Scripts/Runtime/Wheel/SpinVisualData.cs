using System;
using Runtime.Common;

namespace Runtime.Reward
{
    [Serializable]
    public struct SpinVisualData
    {
        public SlotVisualData[] slotData;
        public int winnerIndex;
    }
}