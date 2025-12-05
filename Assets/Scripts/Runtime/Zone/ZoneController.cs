using System;
using Zenject;

namespace Runtime.Zone
{
    public class ZoneController
    {
        public Action<ZoneData> OnNewZone;
        [Inject] private readonly ZoneData[] _zones;
        public int Length => _zones.Length;
        private bool _hasZone = true;


        public bool HasZone => _hasZone;
        
        public int CurrentZoneIndex { get; private set; } = 0;

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
            if (CurrentZoneIndex >= _zones.Length - 1)
            {
                _hasZone = false;
                return;
            }
            
            CurrentZoneIndex++;
            TryGet(CurrentZoneIndex, out var data);
            OnNewZone?.Invoke(data);
        }
        
        public void Reset()
        {
            CurrentZoneIndex = 0;
            TryGet(CurrentZoneIndex, out var data);
            OnNewZone?.Invoke(data);
        }
    }
}