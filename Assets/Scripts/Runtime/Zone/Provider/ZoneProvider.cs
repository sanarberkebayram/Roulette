using UnityEngine;

namespace Runtime.Zone.Provider
{
    public abstract class ZoneProvider : ScriptableObject
    {
        public abstract ZoneData[] GetZones();
    }
}