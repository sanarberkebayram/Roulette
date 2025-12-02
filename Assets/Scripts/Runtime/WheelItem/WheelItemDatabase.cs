using System;
using UnityEngine;

namespace Runtime.WheelItem
{
    [CreateAssetMenu(fileName = "WheelItemDatabase", menuName = "Item/WheelItem/WheelItemDatabase", order = 0)]
    public class WheelItemDatabase : ScriptableObject
    {
        // Wheel Item can be Scriptable Object itself later
        // Its struct for ease of implementation
        [field: SerializeField] public WheelItem[] Items { get; private set; }

        private void OnValidate()
        {
            if (Items == null)
                return;
            
            AssignEmptyUuids();
            AssignEmptyNames();
        }

        void AssignEmptyUuids()
        {
            for (int i = 0; i < Items.Length; i++)
            {
                var item = Items[i];
                if (string.IsNullOrWhiteSpace(item.uuid))
                {
                    Items[i] = new WheelItem()
                    {
                        uuid = Guid.NewGuid().ToString(),
                        name = item.name,
                        sprite = item.sprite,
                        type = item.type
                    };
                }
            }
        }

        void AssignEmptyNames()
        {
            for (int i = 0; i < Items.Length; i++)
            {
                var item = Items[i];
                if (item.sprite && string.IsNullOrWhiteSpace(item.name))
                {
                    Items[i] = new WheelItem()
                    {
                        uuid = Guid.NewGuid().ToString(),
                        name = item.sprite.name,
                        sprite = item.sprite,
                        type = item.type
                    };
                }
            }
        }
    }
}