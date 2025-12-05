using UnityEngine;
using Zenject;

namespace Runtime.Reward
{
    [CreateAssetMenu(fileName = "RewardInstaller", menuName = "Reward/Installer", order = 0)]
    public class RewardInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private RewardCountData[] rewardCountData;
        public override void InstallBindings()
        {
            Container.Bind<RewardInventory>().AsSingle();
            Container.BindInstance(rewardCountData).AsSingle();
            Container.Bind<RewardController>().AsSingle();
        }
        
        #if UNITY_EDITOR
        [ContextMenu("Generate Data Sample")]
        void GenerateDataSample()
        {
            rewardCountData = new RewardCountData[60];
            
            for (int i = 0; i < 60; i++)
            {
                rewardCountData[i] = new RewardCountData()
                {
                    zoneOrder = i + 1,
                    regularItemCountInterval = new Vector2Int(1, 3 * ( i + 1)),
                    silverItemCountInterval = new Vector2Int(1, 3 * ( i + 1)),
                    goldItemCountInterval = new Vector2Int(1, 3 * ( i + 1)),
                };
            }
        }
        #endif
    }
}