using System;
using System.Collections.Generic;

namespace Runtime.EventBus
{
    public class SceneEventBus
    {
        readonly Dictionary<Type, HashSet<Delegate>> _listeners = new();

        public void Subscribe<T>(Action<T> listener) where T : IEvent
        {
            var type = typeof(T);
            if (!_listeners.ContainsKey(type))
                _listeners[type] = new HashSet<Delegate>();

            _listeners[type].Add(listener);
        }

        public void Unsubscribe<T>(Action<T> listener) where T : IEvent
        {
            var type = typeof(T);
            if (_listeners.ContainsKey(type))
                _listeners[type].Remove(listener);
        }

        public void Raise<T>(T @event) where T : IEvent
        {
            var type = typeof(T);
            if (!_listeners.ContainsKey(type))
                return;

            foreach (var listener in _listeners[type])
                (listener as Action<T>)?.Invoke(@event);
        }

        public void Clear() => _listeners.Clear();

        public void Clear<T>() where T : IEvent
        {
            var type = typeof(T);
            if (_listeners.ContainsKey(type))
                _listeners[type].Clear();
        }
    }
}
