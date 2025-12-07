using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

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

        public T Get<T>() where T : MonoBehaviour, IPoolObject
        {
            if (!_pools.TryGetValue(typeof(T), out var poolObject))
                throw new Exception($"Pool for {typeof(T)} does not exist");

            var pool = (ObjectPool<T>)poolObject;
            return pool.Get();
        }

        public void Release<T>(T item) where T : MonoBehaviour, IPoolObject
        {
            if (!_pools.TryGetValue(typeof(T), out var poolObject))
                throw new Exception($"Pool for {typeof(T)} does not exist");

            var pool = (ObjectPool<T>)poolObject;
            pool.Release(item);
        }

    }
}