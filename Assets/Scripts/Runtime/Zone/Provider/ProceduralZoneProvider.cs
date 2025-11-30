using UnityEngine;

namespace Runtime.Zone.Provider
{
    [CreateAssetMenu(fileName = "ProceduralZoneProvider", menuName = "Zone/Provider/Procedural", order = 0)]
    public class ProceduralZoneProvider : ZoneProvider
    {
        [Range(1,60)][SerializeField] private int zoneCount = 60;
        [SerializeField] private ZoneDataEffector[] effectors;
        
        public override ZoneData[] GetZones()
        {
            var zones = new ZoneData[zoneCount];
            for (var i = 0; i < zoneCount; i++)
            {
                var zone = new ZoneData()
                {
                    displayOrder = i + 1,
                    rewardId = string.Empty,
                    type = ZoneType.Regular,
                    isClaimable = false,
                    hasBomb =  false,
                };
            }

            foreach (var effector in effectors)
            {
                effector.Apply(ref zones);
            }

            return zones;
        }
    }
}