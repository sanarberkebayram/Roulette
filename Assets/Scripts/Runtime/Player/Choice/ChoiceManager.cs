using System;
using System.Collections.Generic;
using Runtime.EventBus;

namespace Runtime.Player.Choice
{
    public class ChoiceManager
    {
        private readonly Dictionary<string, IChoice> _choices = new();

        public ChoiceManager(List<IChoice> choices,  SceneEventBus eventBus)
        {
            foreach (var choice in choices)
                _choices.Add(choice.Id, choice);
            
            eventBus.Subscribe<OnChoiceRequest>(HandleChoiceRequest);
        }

        public IChoice GetChoice(string id)
        {
            return !_choices.TryGetValue(id, out var choice) 
                ? throw new Exception($"Choice {id} not found") 
                : choice;
        }
        private void HandleChoiceRequest(OnChoiceRequest obj)
        {
            var choice = GetChoice(obj.id);
            if (!choice.CheckAvailability())
                throw new Exception("Choice selection tried when unavailable");
            choice.Select();
        }
    }
}