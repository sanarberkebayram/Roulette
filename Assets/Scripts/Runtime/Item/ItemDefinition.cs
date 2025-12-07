using System;
using UnityEngine;

namespace Runtime.Item
{
    [Serializable]
    public struct ItemDefinition
    {
        public string uuid;
        public string name;
        public Sprite sprite;
        public ItemType itemType;
    }
}