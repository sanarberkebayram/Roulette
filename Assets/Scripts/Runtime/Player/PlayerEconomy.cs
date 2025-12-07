using System;
using Runtime.EventBus;
using UnityEngine.Events;
using Zenject;

namespace Runtime.Player
{
    [Serializable]
    public class PlayerEconomy
    {
        public int CashAmount => _cashAmount;
        private int _cashAmount;
        [Inject] private SceneEventBus _eventBus;
        
        public PlayerEconomy(int cashAmount, SceneEventBus eventBus)
        {
            _cashAmount = cashAmount;
            _eventBus = eventBus;
        }
        
        public void AddCash(int amount)
        {
            _cashAmount += amount;
            _eventBus.Raise(new MoneyChangeEvent()
            {
                oldValue =  _cashAmount - amount,
                currentValue = _cashAmount
            });
        }

        public void SpendCash(int amount)
        {
            _cashAmount -= amount;
            _eventBus.Raise(new MoneyChangeEvent()
            {
                oldValue =  _cashAmount + amount,
                currentValue = _cashAmount
            });
        }
        
        public bool CheckAffordable(int amount) => amount <= _cashAmount;
    }
}