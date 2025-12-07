using UnityEngine;

namespace Runtime.Reward.Strategy
{
    [CreateAssetMenu(fileName = "DirectRewarder", menuName = "Reward/Direct", order = 0)]
    public class DirectRewardSo : ScriptableObject
    {
        [SerializeField] private SpinData[] spinData;
        public SpinData[] SpinData => spinData;
    }
}
