using UnityEngine;
using Zenject;

namespace Runtime.Game
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<PrepState>().AsSingle().NonLazy();
            Container.Bind<RewardState>().AsSingle().NonLazy();
            Container.Bind<SpinState>().AsSingle().NonLazy();
            Container.Bind<BombState>().AsSingle().NonLazy();
            Container.Bind<ClaimState>().AsSingle().NonLazy();
            Container.Bind<ExitState>().AsSingle().NonLazy();
            Container.Bind<Game>().FromComponentInHierarchy().AsSingle();
        }
    }
}