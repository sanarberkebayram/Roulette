using UnityEngine;

namespace Runtime.Zone.Provider
{
    [CreateAssetMenu(fileName = "ProceduralZoneProvider", menuName = "Zone/Providers/Procedural", order = 0)]
    public class ProceduralZoneProvider : ScriptableObject, IZoneProvider
    {
        [SerializeField] private int zoneCount = 60;
        [SerializeField] private ScriptableObject[] effectorSo;
        public ZoneData[] GetZones()
        {
            var zones = new ZoneData[zoneCount];
            
            for (var i = 0; i < zoneCount; i++)
                zones[i] = ZoneData.Default(order: i + 1);

            foreach (var so in effectorSo)
            {
                var effector = (IZoneDataEffector)so;
                effector.Apply(ref zones);
            }

            return zones;
        }

        void OnValidate()
        {
            if (effectorSo == null || effectorSo.Length == 0)
                return;

            for(int i = effectorSo.Length - 1  ; i >=0 ; i--)
            {
                var so = effectorSo[i];
                if (so == null)
                    continue;

                if (so is IZoneDataEffector effector) 
                    continue;
                
                effectorSo[i] = null;
                Debug.LogError($"{nameof(IZoneProvider)} could not be found.");
            }
        }
    }
}