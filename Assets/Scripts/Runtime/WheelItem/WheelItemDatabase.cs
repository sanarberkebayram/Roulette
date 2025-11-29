using System;
using UnityEngine;

namespace Runtime.WheelItem
{
    [CreateAssetMenu(fileName = "WheelItemDatabase", menuName = "Item/WheelItem/WheelItemDatabase", order = 0)]
    public class WheelItemDatabase : ScriptableObject
    {
        // Wheel Item can be Scriptable Object itself later
        // Its struct for ease of implementation for case
        [field: SerializeField] public WheelItem[] Items { get; private set; }

        private void OnValidate()
        {
            if (Items == null)
                return;
            
            AssignEmptyUuids();
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
    }
}