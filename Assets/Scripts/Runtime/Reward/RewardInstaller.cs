using UnityEngine;
using Zenject;

namespace Runtime.Reward
{
    [CreateAssetMenu(fileName = "RewardInstaller", menuName = "Reward/Installer", order = 0)]
    public class RewardInstaller : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<RewardInventory>().AsSingle();
        }
    }
}