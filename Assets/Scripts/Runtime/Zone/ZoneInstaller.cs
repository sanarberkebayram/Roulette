using JetBrains.Annotations;
using Runtime.Zone.Provider;
using UnityEngine;
using Zenject;

namespace Runtime.Zone
{
    [CreateAssetMenu(fileName = "ZoneInstaller", menuName = "Zone/Installer", order = 0)]
    public class ZoneInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private ScriptableObject providerSo;
        
        #if UNITY_EDITOR
        [UsedImplicitly] private ZoneData[] _debugView;
        #endif
        
        public override void InstallBindings()
        {
            var provider = (IZoneProvider) providerSo;
            #if UNITY_EDITOR
            _debugView = provider.GetZones();
            #endif
            Container.Bind<ZoneData[]>().FromInstance(provider.GetZones()).AsTransient();
            Container.Bind<ZoneController>().AsSingle();
        }

        void OnValidate()
        {
            if (providerSo == null)
                return;
            
            var provider = (IZoneProvider) providerSo;
            if (provider != null)
                return;

            providerSo = null;
            Debug.LogError($"{nameof(IZoneProvider)} could not be found.");
        }
    }
}