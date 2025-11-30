using UnityEngine;

namespace Runtime.Zone.Provider
{
    [CreateAssetMenu(fileName = "DirectZoneProvider", menuName = "Zone/Provider/Direct", order = 0)]
    public class DirectZoneProvider : ZoneProvider
    {
        [SerializeField] private ZoneData[] zones;

        public override ZoneData[] GetZones()
            => zones;
    }
}