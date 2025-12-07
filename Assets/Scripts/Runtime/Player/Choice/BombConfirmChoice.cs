using Runtime.EventBus;
using Runtime.Popup;

namespace Runtime.Player.Choice
{
    public class BombConfirmChoice : IChoice
    {
        public string Id => "BombConfirmChoice";
        private IChoiceInteractable _interactable;
        private readonly BombPopup _popup;
        private readonly SceneEventBus _eventBus;

        public BombConfirmChoice(BombPopup popup, SceneEventBus eventBus)
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