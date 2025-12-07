using Runtime.Reward.Strategy;
using Runtime.Reward.UI;
using UnityEngine;
using Zenject;

namespace Runtime.Reward
{
    [CreateAssetMenu(fileName = "RewardInstaller", menuName = "Reward/Installer", order = 0)]
    public class RewardInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private RewardStrategyType strategyType = RewardStrategyType.Direct;
        [SerializeField] private DirectRewardSo directProvider;
        [SerializeField] private ProceduralRewardSo proceduralProvider;
        [SerializeField] private float rewardMultiplier = 1;

        [Header("Reward Animation")] 
        [SerializeField] private float firstDistance;
        
        public override void InstallBindings()
        {
            Container.Bind<RewardAnimator>().AsSingle().WithArguments(firstDistance);
            Container.Bind<RewardUI>().FromComponentInHierarchy().AsSingle();
            Container.BindInterfacesAndSelfTo<RewardInventory>()
                .AsSingle()
                .WithArguments("reward_inventory");            
            
            Container.Bind<RewardController>().AsSingle().WithArguments(rewardMultiplier);
            switch (strategyType)
            {
                case RewardStrategyType.Direct:
                    BindDirectStrategy();
                    break;
                case RewardStrategyType.Procedural:
                    BindProceduralStrategy();
                    break;
                default:
                    BindDirectStrategy();
                    break;
            }

        }


        void BindDirectStrategy()
        {
            if (directProvider == null)
            {
                Debug.LogError("Direct reward provider is not assigned.", this);
                return;
            }
            
            Container.Bind<SpinData[]>().FromInstance(directProvider.SpinData).AsSingle();
            Container.Bind<IRewardStrategy>().To<DirectRewarder>().AsSingle();
        }

        void BindProceduralStrategy()
        {
            if (proceduralProvider == null)
            {
                Debug.LogError("Procedural reward provider is not assigned.", this);
                return;
            }

            Container.Bind<ProceduralRewardSo>().FromInstance(proceduralProvider).AsSingle();
            Container.Bind<IRewardStrategy>().To<ProceduralRewarder>().AsSingle();
        }
    }

    public enum RewardStrategyType
    {
        Direct,
        Procedural
    }
}
