using System;
using UnityEngine;

namespace Runtime.WheelItem
{
    [Serializable]
    public struct WheelItem
    {
        public string uuid;
        public string name;
        public WheelItemType type;
        public Sprite sprite;
   }
}
