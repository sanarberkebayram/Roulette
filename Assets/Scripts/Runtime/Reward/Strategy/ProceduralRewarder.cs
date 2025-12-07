using Runtime.Zone;
using UnityEngine;

namespace Runtime.Reward.Strategy
{
    public class ProceduralRewarder : IRewardStrategy
    {
        private readonly ZoneController _zoneController;
        private readonly ProceduralRewardSo _settings;

        public ProceduralRewarder(ZoneController zoneController, ProceduralRewardSo settings)
        {
            _zoneController = zoneController;
            _settings = settings;
        }

        public SpinData GetSpinData()
        {
            if (_settings == null)
            {
                Debug.LogError("Procedural reward settings are missing.");
                return default;
            }

            return _settings.CreateSpinData(_zoneController.CurrentZone);
        }

    }
}
