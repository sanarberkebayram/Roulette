using Runtime.Popup;

namespace Runtime.Game
{
    public class BombState : IGameState
    {
        private readonly BombPopup _bombPopup;
        private readonly Game _game;

        public BombState(BombPopup bombPopup, Game game)
        {
            _bombPopup = bombPopup;
            _game = game;
            StateLookup.BombState = this;
        }

        public void OnEnter()
        {
            _bombPopup.Show();
            _bombPopup.OnPopupFinish.AddListener(HandlePopupFinish);
        }


        public void OnExit()
        {
            _bombPopup.OnPopupFinish.RemoveListener(HandlePopupFinish);
        }

        public void OnUpdate()
        {
        }
        
        private void HandlePopupFinish()
        {
            _game.ChangeState(StateLookup.PrepState);
        }
    }
}