using UnityEngine.Events;

namespace Runtime.Player
{
    public class PlayerEconomy
    {
        public UnityAction<int,int> OnCashChange;
        private int _cashAmount;
        public int CashAmount => _cashAmount;
        
        public PlayerEconomy(int cashAmount)
        {
            _cashAmount = cashAmount;
        }
        
        public void AddCash(int amount)
        {
            _cashAmount += amount;
        }

        public void SpendCash(int amount)
        {
            _cashAmount -= amount;
        }
        
        public bool CheckAffordable(int amount) => amount <= _cashAmount;
    }
}