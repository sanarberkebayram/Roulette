using System;
using Runtime.EventBus;
using Runtime.Player.Choice;
using Zenject;

namespace Runtime.Zone
{
    public class ZoneController
    {
        public ZoneData CurrentZone { get; private set; }
        private readonly ZoneData[] _zones;
        private readonly SceneEventBus _eventBus;
        private int _currentZoneIndex;

        public ZoneController(ZoneData[] zones, SceneEventBus eventBus)
        {
            _zones = zones;
            _eventBus = eventBus;
            _currentZoneIndex = 0;
            CurrentZone = _zones[_currentZoneIndex];
            
            _eventBus.Subscribe<RestartEvent>(HandleRestart);
        }


        public bool CanIncreaseZone()
        {
            if (_zones == null || _zones.Length == 0)
                return false;
            
            return _currentZoneIndex < _zones.Length-1;
        }

        public void IncreaseZone()
        {
            var oldZone = _currentZoneIndex >= 0 ? _zones[_currentZoneIndex] : default;
            _currentZoneIndex++;
            CurrentZone = _zones[_currentZoneIndex];
            
            _eventBus.Raise(
                new ZoneChangeEvent()
                {
                    oldZone = oldZone,
                    newZone = CurrentZone,
                }
            );
        }

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

        private void HandleRestart(RestartEvent obj)
        {
            _currentZoneIndex = 0;
            CurrentZone = _zones[_currentZoneIndex];
            _eventBus.Raise(new ZoneResetEvent());
        }
    }
}