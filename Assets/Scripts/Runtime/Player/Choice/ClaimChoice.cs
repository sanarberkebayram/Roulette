using Runtime.EventBus;
using Runtime.Game;
using Runtime.Zone;

namespace Runtime.Player.Choice
{
    public class ClaimChoice : IChoice
    {
        public string Id => "ClaimChoice";
        private readonly ZoneController _zoneController;
        private readonly IStateManager _gameManager;
        private readonly SceneEventBus _eventBus;
        
        private IChoiceInteractable _interactable;

        public ClaimChoice(
            ZoneController zoneController,
            IStateManager gameManager,
            SceneEventBus eventBus
            )
        {
            _zoneController = zoneController;
            _gameManager = gameManager;
            _eventBus = eventBus;
            _eventBus.Subscribe<ZoneChangeEvent>(HandleZoneChange);
            _eventBus.Subscribe<StateChangeEvent>(HandleStateChange);
        }


        public void SetInteractable(IChoiceInteractable interactable)
        {
            _interactable = interactable;
            CheckAvailability();
        }

        public bool CheckAvailability()
        {
            var zone = _zoneController.CurrentZone;
            var available = zone.isClaimable && _gameManager.CurrentState == StateType.Idle;
            _interactable.Toggle(available, "CLAIM");
            return available;
        }

        public void Select()
        {
            _eventBus.Raise(new ClaimRequestEvent());
        }
        
        private void HandleStateChange(StateChangeEvent obj)
        {
            CheckAvailability();
        }

        private void HandleZoneChange(ZoneChangeEvent obj)
        {
            CheckAvailability();
        }
    }
}