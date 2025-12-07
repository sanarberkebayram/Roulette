using UnityEngine;
using Zenject;

namespace Runtime.Item
{
    [CreateAssetMenu(fileName = "ItemInstaller", menuName = "Item/Installer")]
    public class ItemInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private ItemDatabaseSo so;

        public override void InstallBindings()
        {
            so.BuildLookup();
            Container.Bind<IItemDatabase>().FromInstance(so).AsSingle();
        }
    }
}