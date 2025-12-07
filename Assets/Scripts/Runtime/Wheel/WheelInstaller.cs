using Runtime.Common.UI;
using Zenject;

namespace Runtime.Common
{
    public class WheelInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<WheelController>().AsSingle().NonLazy();
            Container.Bind<WheelUI>().FromComponentInHierarchy().AsSingle().NonLazy();
        }
    }
}