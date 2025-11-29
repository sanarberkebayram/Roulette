using UnityEngine;
using Zenject;

namespace Runtime.ObjectPooler
{
    [CreateAssetMenu(fileName = "PoolInstaller", menuName = "Pool/Installer", order = 0)]
    public class PoolInstaller : ScriptableObjectInstaller
    {
        [SerializeField] public PoolSettings settings;
        [Inject] private PoolManager _poolManager;

        public override void InstallBindings()
        {
        }
    }
    
}