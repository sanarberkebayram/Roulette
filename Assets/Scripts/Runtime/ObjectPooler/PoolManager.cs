using System;
using System.Collections.Generic;
using UnityEngine;

namespace Runtime.ObjectPooler
{
    public class PoolManager
    {
        private readonly Dictionary<Type, object> _pools = new Dictionary<Type, object>();
        
        public void AddPool<T>(PoolCreator<T> creator) where T : MonoBehaviour, IPoolObject
        {
            if (_pools.ContainsKey(typeof(T)))
                throw new Exception($"Pool for {typeof(T)} already exists");
            
            _pools.Add(typeof(T), creator.CreatePool());
        }

        public T GetPool<T>() where T : MonoBehaviour, IPoolObject
        {
            if (!_pools.TryGetValue(typeof(T), out var pool))
                throw new Exception($"Pool for {typeof(T)} does not exist");
            
            return (T) pool;
        }
   }
}