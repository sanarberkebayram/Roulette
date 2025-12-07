using Runtime.EventBus;
using UnityEngine;
using Zenject;

namespace Runtime.Zone.UI
{
    public class ZonePanelController : MonoBehaviour
    {
        [SerializeField] private ZonePanelCenter centerPanel;
        [SerializeField] private ZonePanelBack backPanel;
        [SerializeField] private bool initializeSilently = true;

        [Inject] private SceneEventBus _eventBus;
        [Inject] private ZoneController _zoneController;

        void OnEnable()
        {
            _eventBus.Subscribe<ZoneChangeEvent>(HandleZoneChange);
            _eventBus.Subscribe<ZoneResetEvent>(HandleZoneReset);
            DisplayInitialZone();
        }

        void OnDisable()
        {
            _eventBus.Unsubscribe<ZoneChangeEvent>(HandleZoneChange);
            _eventBus.Unsubscribe<ZoneResetEvent>(HandleZoneReset);
        }

        private void HandleZoneReset(ZoneResetEvent obj)
        {
            backPanel.Clear();
            DisplayInitialZone();
        }

        void HandleZoneChange(ZoneChangeEvent evt)
        {
            centerPanel.Display(evt.newZone);
            
            var halfSize = backPanel.MaxViewers / 2;
            
            var targetIndex = evt.newZone.displayOrder + halfSize - 1;
            _zoneController.TryGet(targetIndex, out var targetZone);
            backPanel.Display(targetZone);
        }

        void DisplayInitialZone()
        {
            if (_zoneController == null || centerPanel == null)
                return;

            var currentZone = _zoneController.CurrentZone;
            centerPanel.Display(currentZone, initializeSilently);
            backPanel.Display(currentZone, initializeSilently);
            
            var halfSize = backPanel.MaxViewers / 2;
            var currIndex = currentZone.displayOrder;
            
            for (int i = 0; i < halfSize; i++)
            {
                if (!_zoneController.TryGet(currIndex+i, out var zone))
                {
                    backPanel.Display(null, initializeSilently);
                }
                else
                {
                    backPanel.Display(zone, initializeSilently);
                }
            }
        }
    }
}
