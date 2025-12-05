using UnityEngine;
using Zenject;

namespace Runtime.WheelItem
{
    [CreateAssetMenu(fileName = "WheelItemInstaller", menuName = "Item/WheelItem/Installer", order = 0)]
    public class WheelItemInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private WheelItemDatabase database;
        
        public override void InstallBindings()
        {
            database.Initialize();
            Container.Bind<WheelItemDatabase>().FromInstance(database).AsSingle();
        }
    }
}