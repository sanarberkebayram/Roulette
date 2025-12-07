using Runtime.Common.UI;
using Runtime.EventBus;
using Runtime.Game;
using Runtime.Player.Choice;
using Runtime.Reward;
using Runtime.Zone;
using UnityEngine;
using UnityEngine.Events;

namespace Runtime.Common
{
    public class WheelController
    {
        private readonly WheelUI _wheelUI;
        private readonly SceneEventBus _eventBus;
        private readonly ZoneController _zoneController;
        
        private int _spinResult;
        
        public WheelController(WheelUI wheelUI, SceneEventBus eventBus, ZoneController zoneController)
        {
            _wheelUI = wheelUI;
            _eventBus = eventBus;
            _zoneController = zoneController;

            _eventBus.Subscribe<StateChangeEvent>(HandleStateChange);
            _eventBus.Subscribe<ZoneChangeEvent>(HandleZoneChange);
            _eventBus.Subscribe<ZoneResetEvent>(HandleZoneReset);
        }



        public void SetSpinInfo(SpinVisualData visualData)
        {
            _spinResult = visualData.winnerIndex;
            var slotData = visualData.slotData;
            
            for (int i = 0; i < slotData.Length; i++)
            {
                var slot = slotData[i];
                _wheelUI.SetSlot(i, slot.icon, slot.slotText);
            }
        }

        public void SetWheel(ZoneType zone)
        {
            _wheelUI.SetWheel(zone);
        }

        public void Spin()
        {
            _wheelUI.AnimateSpin(_spinResult);
        }
        
        private void HandleStateChange(StateChangeEvent evt)
        {
            if (evt.newState == StateType.Idle)
            {
                _wheelUI.AnimateIdle();
            }
        }
        
        private void HandleZoneChange(ZoneChangeEvent obj)
        {
            SetWheel(obj.newZone.type);
        }
        
        private void HandleZoneReset(ZoneResetEvent obj)
        {
            SetWheel(_zoneController.CurrentZone.type);
        }
    }
}