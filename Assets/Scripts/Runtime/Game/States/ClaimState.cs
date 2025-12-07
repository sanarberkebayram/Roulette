using Runtime.EventBus;
using Runtime.Player.Choice;
using Runtime.Popup;

namespace Runtime.Game
{
    public class ClaimState : IState
    {
        public StateType StateType => StateType.Claim;
        
        private readonly ClaimPopup _claimPopup;
        private readonly SceneEventBus _eventBus;
        private readonly IStateManager _stateManager;

        public ClaimState(ClaimPopup popup, SceneEventBus eventBus, IStateManager stateManager)
        {
            _claimPopup = popup;
            _eventBus = eventBus;
            _stateManager = stateManager;
            
            _eventBus.Subscribe<RestartRequestEvent>(HandleRestartClick);
        }

        public void OnEnter()
        {
            _claimPopup.Show();
        }


        public void OnExit()
        {
        }
        
        private void HandleRestartClick(RestartRequestEvent obj)
        {
            _eventBus.Raise(new RestartEvent());
            _stateManager.ChangeState(StateType.Idle);
        }
    }
}