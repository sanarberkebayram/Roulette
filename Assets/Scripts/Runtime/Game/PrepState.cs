using Runtime.Player;
using Runtime.Reward;
using Runtime.Wheel;
using Runtime.Zone;
using UnityEngine;

namespace Runtime.Game
{
    public class PrepState: IGameState
    {
        private readonly PlayerChoiceController _choiceController;
        private readonly PlayerEconomy _economy;
        private readonly Game _game;
        private readonly ZoneController _zoneController;
        private readonly WheelController _wheelController;
        private readonly RewardController _rewardController;

        public PrepState(PlayerChoiceController choiceController, PlayerEconomy economy, Game game, ZoneController zoneController, WheelController wheelController, RewardController rewardController)
        {
            _choiceController = choiceController;
            _economy = economy;
            _game = game;
            _zoneController = zoneController;
            _wheelController = wheelController;
            _rewardController = rewardController;
            
            StateLookup.PrepState = this;
        }


        public void OnEnter()
        {
            _zoneController.IncreaseZone();
            AdjustPlayerChoices();
            AdjustWheel();
            LinkEvents();
        }

        private void AdjustWheel()
        {
            if (!_zoneController.HasZone)
                return;
            
            var spinInfo = _rewardController.GetSpinInfo();
            _wheelController.AnimateIdle();
            _wheelController.SetSpinInfo(spinInfo);
        }

        public void OnUpdate()
        {
        }
        
        public void OnExit()
        {
            UnlinkEvents();
        }

        private void LinkEvents()
        {
            _choiceController.OnSpinClicked.AddListener(HandleSpinClick);
            _choiceController.OnClaimClicked.AddListener(HandleClaimClick);
        }

        void AdjustPlayerChoices()
        {
            var hasZone = _zoneController.HasZone;
            var canSpin = hasZone && _economy.CheckAffordable(_game.SpinCost);
            _choiceController.ToggleChoice(ChoiceType.Spin, !canSpin);

            if (!hasZone)
            {
                _choiceController.ToggleChoice(ChoiceType.Claim,false);
                return;
            }
            
            _zoneController.TryGet(_zoneController.CurrentZoneIndex, out var zone);
            _choiceController.ToggleChoice(ChoiceType.Claim, zone.isClaimable);
        }


        private void UnlinkEvents()
        {
            _choiceController.OnSpinClicked.AddListener(HandleSpinClick);
            _choiceController.OnClaimClicked.AddListener(HandleClaimClick);
        }

        private void HandleClaimClick()
        {
            _game.ChangeState(StateLookup.ClaimState);
        }

        private void HandleSpinClick()
        {
            _game.ChangeState(StateLookup.SpinState);
        }

    }
}