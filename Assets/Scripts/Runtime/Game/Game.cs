using Runtime.EventBus;
using UnityEngine;
using Zenject;

namespace Runtime.Game
{
    public class Game : MonoBehaviour
    {
        [Inject] private IStateManager _stateManager;
        
        [Inject] private StateType _startingState;
        [Inject(Id = "spin_cost")] private int _spinCost;
        [Inject] private SceneEventBus _eventBus;
        public int SpinCost => _spinCost;

        void Start()
        {
            _stateManager.ChangeState(_startingState);
        }

        void OnEnable()
        {
            _eventBus.Subscribe<StateChangeEvent>(HandleStateChange);
            transform.name = $"Game - State[{_stateManager.CurrentState}]";
        }

        void OnDisable()
        {
            _eventBus.Unsubscribe<StateChangeEvent>(HandleStateChange);
        }

        private void HandleStateChange(StateChangeEvent obj)
        {
            transform.name = $"Game - State[{obj.newState}]";
        }
    }
}