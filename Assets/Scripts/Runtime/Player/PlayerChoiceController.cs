using Runtime.Player.UI;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace Runtime.Player
{
    public class PlayerChoiceController : MonoBehaviour
    {
        [SerializeField] private ToggleButton spinButton;
        [SerializeField] private ToggleButton claimButton;

        [HideInInspector] public UnityEvent OnSpinClicked;
        [HideInInspector] public UnityEvent OnClaimClicked;

        private bool _spinBlocked;
        private bool _claimBlocked;

        public void ToggleChoice(ChoiceType choiceType, bool shouldBlock)
        {
            if (choiceType == ChoiceType.Spin) 
            {
                _spinBlocked = shouldBlock;
                spinButton.Toggle(!_spinBlocked, "Spin", () => OnSpinClicked?.Invoke());
                return;
            }
            
            _claimBlocked = shouldBlock;
            claimButton.Toggle(!_claimBlocked, "Claim", () => OnClaimClicked?.Invoke());
        }
    }

    public enum ChoiceType
    {
        Spin, Claim
    }
}