using UnityEngine;
using Zenject;

namespace Runtime.Player
{
    [CreateAssetMenu(fileName = "PlayerInstaller", menuName = "Player/Installer", order = 0)]
    public class PlayerInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private int startAmount;

        public override void InstallBindings()
        {
            Container.Bind<PlayerEconomy>()
                .FromInstance(new PlayerEconomy(startAmount))
                .AsSingle();
            
            Container.Bind<PlayerChoiceController>().FromComponentInHierarchy().AsSingle();
        }
    }
}