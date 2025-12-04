using Runtime.Wheel.UI;
using Zenject;

namespace Runtime.Wheel
{
    public class WheelInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<WheelController>().AsSingle();
            Container.Bind<WheelUI>().FromComponentInHierarchy().AsSingle();
        }
    }
}