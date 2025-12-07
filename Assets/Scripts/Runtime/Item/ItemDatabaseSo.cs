using System;
using System.Collections.Generic;
using UnityEngine;

namespace Runtime.Item
{
    [CreateAssetMenu(fileName = "ItemDatabase", menuName = "Item/Database")]
    public class ItemDatabaseSo :  ScriptableObject, IItemDatabase
    {
        [SerializeField] private List<ItemDefinition> items;
        [SerializeField] private Sprite loader;
        private Dictionary<string, ItemDefinition> _lookUp;
        public IReadOnlyList<ItemDefinition> Items => items;
        

        public void BuildLookup()
        {
            _lookUp = new Dictionary<string, ItemDefinition>();
            foreach (var item in items)
                _lookUp.Add(item.uuid, item);
        }
        
        public void AddItem(ItemDefinition item)
        {
            _lookUp?.TryAdd(item.uuid, item);
        }
        public void RemoveItem(ItemDefinition item)
        {
            _lookUp?.Remove(item.uuid);
        }
        public bool TryGetItem(string uuid, out ItemDefinition item)
        {
            return _lookUp.TryGetValue(uuid, out item);
        }

        private void OnValidate()
        {
            for (int i = 0; i < items.Count; i++)
            {
                var item = items[i];
                
                if (string.IsNullOrWhiteSpace(item.uuid))
                    item.uuid = Guid.NewGuid().ToString();

                if (item.sprite != null && string.IsNullOrWhiteSpace(item.name))
                    item.name = item.sprite.name;
                items[i] = item;
            }

            if (loader == null)
                return;

            var newItem = new ItemDefinition()
            {
                uuid = Guid.NewGuid().ToString(),
                itemType = ItemType.Regular,
                name = loader.name,
                sprite = loader
            };
            
            items.Add(newItem);
            loader = null;
        }
    }
}
