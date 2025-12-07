using System.Collections.Generic;
using UnityEngine;

namespace Runtime.Zone.Provider
{
    public interface IZoneDataEffector
    {
        public void Apply(ref ZoneData[] zoneData);
    }
}