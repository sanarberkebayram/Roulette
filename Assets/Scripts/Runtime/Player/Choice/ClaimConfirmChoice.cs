using Runtime.EventBus;
using Runtime.Popup;

namespace Runtime.Player.Choice
{
    public class ClaimConfirmChoice : IChoice
    {
        public string Id => "ClaimConfirmChoice";
        private IChoiceInteractable _interactable;
        private readonly ClaimPopup _popup;
        private readonly SceneEventBus _eventBus;

        public ClaimConfirmChoice(ClaimPopup popup, SceneEventBus eventBus)
        {
            _popup = popup;
            _eventBus = eventBus;
        }

        public void SetInteractable(IChoiceInteractable interactable)
        {
            _interactable = interactable;
            CheckAvailability();
        }

        public bool CheckAvailability()
        {
            _interactable.Toggle(true, "RESTART");
            return true;
        }

        public void Select()
        {
            _popup.Hide();
            _eventBus.Raise(new RestartRequestEvent());
        }
    }
}