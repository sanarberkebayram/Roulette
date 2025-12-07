using UnityEngine;

namespace Runtime.Zone.Provider
{
    [CreateAssetMenu(fileName = "DirectZoneProvider", menuName = "Zone/Providers/Direct", order = 0)]
    public class DirectZoneProvider : ScriptableObject, IZoneProvider
    {
        [SerializeField] private ZoneData[] zones;

        public ZoneData[] GetZones()
            => zones;
    }
}