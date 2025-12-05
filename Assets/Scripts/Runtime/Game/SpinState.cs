using Runtime.Player;
using Runtime.Wheel;
using UnityEngine;

namespace Runtime.Game
{
    public class SpinState : IGameState
    {
        private readonly PlayerChoiceController _choiceController;
        private readonly WheelController _wheelController;
        private readonly Game _game;

        public SpinState(PlayerChoiceController choiceController, WheelController wheelController, Game game, RewardState rewardState, BombState bombState)
        {
            _choiceController = choiceController;
            _wheelController = wheelController;
            _game = game;
            
            StateLookup.SpinState = this;
        }

        public void OnEnter()
        {
            DisableChoices();
            _wheelController.Spin();
            _wheelController.OnSpinComplete.AddListener(HandleSpinComplete);
        }

        public void OnExit()
        {
            _wheelController.OnSpinComplete.AddListener(HandleSpinComplete);
        }


        public void OnUpdate()
        {
        }
        
        private void HandleSpinComplete()
        {
            _game.ChangeState(_wheelController.IsBombExploded ? StateLookup.RewardState : StateLookup.BombState);
        }

        void DisableChoices()
        {
            _choiceController.ToggleChoice(ChoiceType.Spin, true);
            _choiceController.ToggleChoice(ChoiceType.Claim, true);
        }
    }
}