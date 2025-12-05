using Zenject;

namespace Runtime.Popup
{
    public class PopupInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<BombPopup>().FromComponentInHierarchy(true).AsSingle();
        }
    }
}