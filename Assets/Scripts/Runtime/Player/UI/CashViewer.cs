using System;
using Runtime.EventBus;
using TMPro;
using UnityEngine;
using Zenject;

namespace Runtime.Player.UI
{
    public class CashViewer : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI cashText;
        [Inject] private PlayerEconomy _economy;
        [Inject] private SceneEventBus _eventBus;

        private void HandleCashChange(MoneyChangeEvent evt)
        {
            cashText.SetText(evt.currentValue.ToString());
        }

        private void OnEnable()
        {
            cashText.SetText(_economy.CashAmount.ToString());
            _eventBus.Subscribe<MoneyChangeEvent>(HandleCashChange);
        }
        private void OnDisable()
        {
            _eventBus.Unsubscribe<MoneyChangeEvent>(HandleCashChange);
        }

        private void OnValidate()
        {
            cashText = GetComponentInChildren<TextMeshProUGUI>();
        }
    }
}