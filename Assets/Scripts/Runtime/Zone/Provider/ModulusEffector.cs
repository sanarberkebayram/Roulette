using UnityEngine;

namespace Runtime.Zone.Provider
{
    [CreateAssetMenu(fileName = "ModulusEffector", menuName = "Zone/Provider/ModulusEffector", order = 0)]
    public class ModulusEffector : ScriptableObject, IZoneDataEffector
    {
        [SerializeField] private int applyToEveryN = 5;
        [SerializeField] private ZoneType zoneType = ZoneType.Silver;
        [SerializeField] private bool isClaimable;
        [SerializeField] private bool hasBomb;
        [SerializeField] private float rewardMultiplier;
        
        public void Apply(ref ZoneData[] zoneData)
        {
            for (var i = 0; i < zoneData.Length; i++)
            {
                if (zoneData[i].displayOrder % applyToEveryN != 0)
                    continue;
                
                zoneData[i].isClaimable = isClaimable;
                zoneData[i].hasBomb = hasBomb;
                zoneData[i].type = zoneType;
                zoneData[i].rewardMultiplier = rewardMultiplier;
            }
        }
    }
}
