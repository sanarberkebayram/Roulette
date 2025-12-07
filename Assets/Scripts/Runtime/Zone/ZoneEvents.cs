using System;
using Runtime.EventBus;

namespace Runtime.Zone
{
    public struct ZoneResetEvent : IEvent { }

    [Serializable]
    public struct ZoneChangeEvent : IEvent
    {
        public ZoneData oldZone;
        public ZoneData newZone;
    }
}