namespace Runtime.Player.Choice
{
    public class HesoyamChoice : IChoice
    {
        public string Id => "HESOYAM";
        private readonly PlayerEconomy _economy;
        private readonly int _amount;
        private IChoiceInteractable _interactable;

        public HesoyamChoice(PlayerEconomy economy, int amount)
        {
            _economy = economy;
            _amount = amount;
        }

        public void SetInteractable(IChoiceInteractable interactable)
        {
            _interactable = interactable;
            CheckAvailability();
        }

        public bool CheckAvailability()
        {
            return true;
        }

        public void Select()
        {
            _economy.AddCash(_amount);
        }
    }
}