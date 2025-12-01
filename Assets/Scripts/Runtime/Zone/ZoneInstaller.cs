using Runtime.Zone.Provider;
using UnityEngine;
using Zenject;

namespace Runtime.Zone
{
    public class ZoneInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private ZoneProvider provider;

        public override void InstallBindings()
        {
            Container.Bind<ZoneData[]>().FromInstance(provider.GetZones()).AsTransient();
            Container.Bind<ZoneController>().AsSingle();
        }
    }
}