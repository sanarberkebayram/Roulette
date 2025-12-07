using Runtime.Player.Choice;
using UnityEngine;
using Zenject;

namespace Runtime.Player
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private int startAmount;
        [SerializeField] private int hesoyamAmount;

        public override void InstallBindings()
        {
            Container.Bind<PlayerEconomy>()
                .AsSingle()
                .WithArguments(startAmount);

            InstallChoices();
        }

        void InstallChoices()
        {
            Container.Bind<IChoice>().To<HesoyamChoice>().AsSingle().WithArguments(hesoyamAmount).NonLazy();
            Container.Bind<IChoice>().To<SpinChoice>().AsSingle().NonLazy();
            Container.Bind<IChoice>().To<ClaimChoice>().AsSingle().NonLazy();
            Container.Bind<IChoice>().To<ClaimConfirmChoice>().AsSingle().NonLazy();
            Container.Bind<IChoice>().To<BombConfirmChoice>().AsSingle().NonLazy();
            
            Container.BindInterfacesAndSelfTo<ChoiceManager>().AsSingle().NonLazy();
        }
    }
}