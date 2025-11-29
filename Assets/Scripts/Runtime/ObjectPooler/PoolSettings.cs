using System;

namespace Runtime.ObjectPooler
{
    [Serializable]
    public struct PoolSettings
    {
        public int defaultCapacity;
        public int maxSize;
    }
}