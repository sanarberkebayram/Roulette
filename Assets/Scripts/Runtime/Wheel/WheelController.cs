using Runtime.Reward;
using Runtime.Wheel.UI;
using Runtime.Zone;
using UnityEngine.Events;

namespace Runtime.Wheel
{
    public class WheelController
    {
        private readonly WheelUI _wheelUI;
        private readonly ZoneController _zoneController;
        private SpinInfo _spinInfo;
        public bool IsBombExploded => _spinInfo.bombExploded;
        public UnityEvent OnSpinComplete => _wheelUI.onAnimationFinish;
        public WheelController(WheelUI wheelUI)
        {
            _wheelUI = wheelUI;
        }

        public void AnimateIdle() => _wheelUI.AnimateIdle();

        public void SetSpinInfo(SpinInfo info)
        {
            _spinInfo = info;
            for (int i = 0; i < info.rewards.Length; i++)
            {
                var reward = info.rewards[i];
                _wheelUI.SetSlot(i, reward.uuid, reward.amount);
            }
        }

        public void SetWheel(ZoneType zone)
        {
            _wheelUI.SetWheel(zone);
        }

        public void Spin()
        {
            _wheelUI.AnimateSpin(_spinInfo.winnerIndex);
        }
    }
}