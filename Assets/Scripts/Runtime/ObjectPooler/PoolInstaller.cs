using Runtime.Player.UI;
using Runtime.Reward.UI;
using Runtime.Zone.UI;
using UnityEngine;
using Zenject;

namespace Runtime.ObjectPooler
{
    [CreateAssetMenu(fileName = "PoolInstaller", menuName = "Pool/Installer", order = 0)]
    public class PoolInstaller : ScriptableObjectInstaller
    {
        [Header("Settings")]
        [SerializeField] private PoolSettings settings;

        [Header("References")] 
        [SerializeField] private RegularZoneViewer zoneViewer;
        [SerializeField] private ZoneCenterViewer zoneCenterViewer;
        [SerializeField] private RewardUISlot rewardSlot;
        [SerializeField] private ToggleButton toggleButton;
        

        public override void InstallBindings()
        {
            Container.BindInstance(CreatePoolManager()).AsSingle();
        }

        private PoolManager CreatePoolManager()
        {
            var manager = new PoolManager();
            manager.AddPool(new PoolCreator<RegularZoneViewer>(zoneViewer, settings));
            manager.AddPool(new PoolCreator<ZoneCenterViewer>(zoneCenterViewer, settings));
            manager.AddPool(new PoolCreator<RewardUISlot>(rewardSlot, settings));
            manager.AddPool(new PoolCreator<ToggleButton>(toggleButton, settings));
            return manager;
        }
    }
    
}