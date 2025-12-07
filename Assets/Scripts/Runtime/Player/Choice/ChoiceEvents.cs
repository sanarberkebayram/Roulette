using System;
using Runtime.EventBus;

namespace Runtime.Player.Choice
{
    [Serializable]
    public struct OnChoiceRequest : IEvent
    {
        public string id;
    }
    
    public struct SpinRequestEvent : IEvent { };
    public struct ClaimRequestEvent : IEvent { };
    public struct RestartRequestEvent : IEvent { };
    public struct RestartEvent : IEvent { };
}