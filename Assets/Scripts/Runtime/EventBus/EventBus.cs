using System;
using System.Collections.Generic;

namespace Runtime.EventBus
{
    public static class EventBus<T> where T : IEvent
    {
        static readonly HashSet<Action<T>> _listeners = new();
        public static void Subscribe(Action<T> listener) => _listeners.Add(listener);
        public static void Unsubscribe(Action<T> listener) => _listeners.Remove(listener);

        public static void Raise(T @event)
        {
            foreach (var listener in _listeners)
                listener?.Invoke(@event);
        }

        public static void Clear() => _listeners.Clear();
    }
}
