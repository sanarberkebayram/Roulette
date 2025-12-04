using System;
using TMPro;
using UnityEngine;
using Zenject;

namespace Runtime.Player.UI
{
    public class CashViewer : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI cashText;
        [Inject] private PlayerEconomy _economy;


        private void HandleCashChange(int _, int current)
        {
            cashText.SetText(current.ToString());
        }

        private void OnEnable()
        {
            cashText.SetText(_economy.CashAmount.ToString());
            _economy.OnCashChange += HandleCashChange;
        }
        private void OnDisable()
        {
            _economy.OnCashChange -= HandleCashChange;
        }

        private void OnValidate()
        {
            cashText = GetComponentInChildren<TextMeshProUGUI>();
        }
    }
}