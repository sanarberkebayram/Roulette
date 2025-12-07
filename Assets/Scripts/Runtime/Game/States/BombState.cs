using Runtime.Common;
using Runtime.EventBus;
using Runtime.Player.Choice;
using Runtime.Popup;

namespace Runtime.Game
{
    public class BombState : IState
    {
        public StateType StateType => StateType.Bomb;
        
        private readonly BombPopup _bombPopup;
        private readonly SceneEventBus _eventBus;
        private readonly IStateManager _stateManager;

        public BombState(BombPopup bombPopup, SceneEventBus eventBus, IStateManager stateManager)
        {
            _bombPopup = bombPopup;
            _eventBus = eventBus;
            _stateManager = stateManager;
            _eventBus.Subscribe<RestartRequestEvent>(HandleRestartClick);
        }


        public void OnEnter()
        {
            _bombPopup.Show();
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