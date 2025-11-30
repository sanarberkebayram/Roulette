using System.Collections.Generic;
using UnityEngine;

namespace Runtime.Zone.Provider
{
    public abstract class ZoneDataEffector : ScriptableObject
    {
        public abstract void Apply(ref ZoneData[] zoneData);
    }
}