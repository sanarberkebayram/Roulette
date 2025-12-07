using Zenject;

namespace Runtime.EventBus
{
    public class EventBusInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<SceneEventBus>().AsSingle().NonLazy();
        }
    }
}
