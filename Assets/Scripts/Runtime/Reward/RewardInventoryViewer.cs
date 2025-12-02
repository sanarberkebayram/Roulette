using UnityEngine;
using Zenject;

namespace Runtime.Reward
{
    public class RewardInventoryViewer : MonoBehaviour
    {
        [SerializeField] private Transform wow;
        [Inject] private RewardInventory _inventory;

        void OnEnable()
        {
            _inventory.OnInventoryChange += HandleInventoryChange;
            _inventory.OnInventoryClear += HandleInventoryClear;
        }

        private void HandleInventoryClear()
        {
        }

        private void HandleInventoryChange(InventoryChange obj)
        {
        }

        void OnDisable()
        {
            _inventory.OnInventoryChange -= HandleInventoryChange;
            _inventory.OnInventoryClear -= HandleInventoryClear;
        }
    }
}