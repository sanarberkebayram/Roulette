using System.Collections.Generic;
using Zenject;

namespace Runtime.Game
{
    public class StateBinder : IInitializable
    {
        private readonly IStateManager _stateManager;
        private readonly List<IState> _states;

        public StateBinder(IStateManager stateManager, List<IState> states)
        {
            _stateManager = stateManager;
            _states = states;
        }

        public void Initialize()
        {
            foreach (var state in _states)
                _stateManager.AddState(state);
        }
    }}