using UnityEngine;
using Zenject;

namespace Runtime.Game
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private int spinMoney = 200;
        [SerializeField] private StateType startingState = StateType.Idle;
        
        public override void InstallBindings()
        {
            Container.Bind<IState>().To<InitializeState>().AsSingle();
            Container.Bind<IState>().To<BombState>().AsSingle();
            Container.Bind<IState>().To<IdleState>().AsSingle();
            Container.Bind<IState>().To<RewardState>().AsSingle();
            Container.Bind<IState>().To<SpinState>().AsSingle();
            Container.Bind<IState>().To<ClaimState>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<StateBinder>().AsSingle().NonLazy();
            
            Container.Bind<StateType>().FromInstance(startingState).AsSingle();
            Container.Bind<IStateManager>().To<StateManager>().AsSingle();
            Container.Bind<int>().WithId("spin_cost").FromInstance(spinMoney).AsSingle();
            Container.Bind<Game>().FromComponentInHierarchy().AsSingle();
        }
    }
}