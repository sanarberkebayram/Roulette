using System;
using Runtime.EventBus;

namespace Runtime.Player
{
    [Serializable]
    public struct MoneyChangeEvent : IEvent
    {
        public int oldValue;
        public int currentValue;
    }
}