using System;
using Zenject;

namespace Runtime.Zone
{
    public class ZoneController
    {
        public Action<ZoneData> OnNewZone;
        public int Length => _zones.Length;
        public int CurrentZoneIndex { get; private set; } = 0;
        [Inject] private readonly ZoneData[] _zones;

        public bool TryGet(int index, out ZoneData data)
        {
            if (index < 0 || index >= _zones.Length)
            {
                data = default;
                return false;
            }
            data = _zones[index];
            return true;
        }

        public void IncreaseZone()
        {
            if (CurrentZoneIndex >= _zones.Length-1)
                return;
            CurrentZoneIndex++;
            TryGet(CurrentZoneIndex, out var data);
            OnNewZone(data);
        }
        
        public void Reset()
        {
            CurrentZoneIndex = 0;
            TryGet(CurrentZoneIndex, out var data);
            OnNewZone?.Invoke(data);
        }
    }
}