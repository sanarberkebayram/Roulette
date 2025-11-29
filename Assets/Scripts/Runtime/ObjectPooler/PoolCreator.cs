using UnityEngine;
using UnityEngine.Pool;
using Object = UnityEngine.Object;

namespace Runtime.ObjectPooler
{
    public class PoolCreator<T> where T: MonoBehaviour, IPoolObject
    {
        private readonly T _instance;
        private readonly PoolSettings _settings;
        public PoolCreator(T instance, PoolSettings settings)
        {
            _instance = instance;
            _settings = settings;
        }

        public ObjectPool<T> CreatePool()
        {
            return new ObjectPool<T>(
                createFunc: () => Object.Instantiate(_instance),
                actionOnGet: item => item.Get(),
                actionOnRelease: item => item.Release(),
                actionOnDestroy: item => item.Destroy(),
                defaultCapacity: _settings.defaultCapacity,
                maxSize: _settings.maxSize
            );
        }

    }
}