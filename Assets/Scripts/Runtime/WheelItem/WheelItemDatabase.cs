using System;
using System.Collections.Generic;
using UnityEngine;

namespace Runtime.WheelItem
{
    [CreateAssetMenu(fileName = "WheelItemDatabase", menuName = "Item/WheelItem/WheelItemDatabase", order = 0)]
    public class WheelItemDatabase : ScriptableObject
    {
        // Wheel Item can be Scriptable Object itself later
        // Its struct for ease of implementation
        [field: SerializeField] public WheelItem[] Items { get; private set; }
        private Dictionary<string, WheelItem> _items;
        
        public WheelItem GetItem(string uuid) => _items[uuid];

        public WheelItem GetBomb()
        {
            foreach (var item in Items)
            {
                if (item.type == WheelItemType.Bomb)
                    return item;
            }
            
            throw new Exception("No bomb found on Wheel Items");
        }
        
        public void Initialize()
        {
            _items = new Dictionary<string, WheelItem>();
            foreach (var item in Items)
                _items.Add(item.uuid, item);
        }

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