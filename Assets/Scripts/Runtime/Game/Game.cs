using UnityEngine;
using Zenject;

namespace Runtime.Game
{
    public class Game : MonoBehaviour
    {
        [SerializeField] private int spinCost;
        public int SpinCost => spinCost;
        
        private IGameState _currentState;
        
        public void ChangeState(IGameState state)
        {
            _currentState?.OnExit();
            _currentState = state;
            _currentState.OnEnter();
        }


        void Start()
        {
            ChangeState(StateLookup.PrepState);
        }

        void Update()
        {
            _currentState?.OnUpdate();
        }
    }
}