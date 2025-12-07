using Runtime.EventBus;
using Runtime.Game;
using Runtime.Zone;
using Zenject;

namespace Runtime.Player.Choice
{
    public class SpinChoice : IChoice
    {
        public string Id => "SpinChoice";
        private readonly PlayerEconomy _economy;
        private readonly IStateManager _gameState;
        private readonly SceneEventBus _bus;
        private readonly ZoneController _zoneController;
        private readonly Game.Game _game;
        private IChoiceInteractable _interactable;

        public SpinChoice(PlayerEconomy economy, IStateManager gameState, SceneEventBus bus, ZoneController zoneController, Game.Game game)
        {
            _economy = economy;
            _gameState = gameState;
            _bus = bus;
            _zoneController = zoneController;
            _game = game;

            ConnectEvents();
        }


        public void SetInteractable(IChoiceInteractable interactable)
        {
            _interactable = interactable;
            CheckAvailability();
        }

        public bool CheckAvailability()
        {
            var available = _economy.CheckAffordable(_game.SpinCost)
                            && _gameState.CurrentState == StateType.Idle
                            && _zoneController.CanIncreaseZone() ;
            _interactable?.Toggle(available,"Spin");
            return available;
        }

        public void Select()
        {
            _economy.SpendCash(_game.SpinCost);
            _bus.Raise(new SpinRequestEvent());
        }

        void ConnectEvents()
        {
            _bus.Subscribe<MoneyChangeEvent>(HandleMoneyChange);
            _bus.Subscribe<StateChangeEvent>(HandleStateChange);
        }

        private void HandleStateChange(StateChangeEvent _)
            => CheckAvailability();

        private void HandleMoneyChange(MoneyChangeEvent _)
            => CheckAvailability();
        
    }

}