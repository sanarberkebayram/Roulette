using Runtime.Zone;

namespace Runtime.Reward.Strategy
{
    public class DirectRewarder : IRewardStrategy
    {
        private readonly SpinData[] _spinData;
        private readonly ZoneController _zoneController;

        public DirectRewarder(ZoneController zoneController, SpinData[] spinData)
        {
            _zoneController = zoneController;
            _spinData = spinData;
        }

        public SpinData GetSpinData()
        {
            var order = _zoneController.CurrentZone.displayOrder;
            return _spinData[order];
        }
    }
}