using System.Data;
using UnityEngine;

namespace Runtime.Zone.Provider
{
    [CreateAssetMenu(fileName = "ModulusEffector", menuName = "Zone/Provider/ModulusEffector", order = 0)]
    public class ModulusEffector : ZoneDataEffector
    {
        [SerializeField] private int applyToEveryN = 5;
        [SerializeField] private ZoneType zoneType = ZoneType.Silver;
        [SerializeField] private string[] specialItemIds;
        [SerializeField] private bool isClaimable;
        [SerializeField] private bool hasBomb;
        public override void Apply(ref ZoneData[] zoneData)
        {
            for (var i = 0; i < zoneData.Length; i++)
            {
                if (i % applyToEveryN != 0) continue;
                
                zoneData[i].type = zoneType;
                zoneData[i].isClaimable = isClaimable;
                zoneData[i].hasBomb = hasBomb;
                zoneData[i].isClaimable = isClaimable;
                
                if (specialItemIds is { Length: > 0 })
                    zoneData[i].rewardId = specialItemIds[Random.Range(0, specialItemIds.Length)];
            }
        }
    }
}