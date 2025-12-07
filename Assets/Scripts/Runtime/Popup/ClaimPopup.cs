using DG.Tweening;
using Runtime.Common;
using Runtime.Player.UI;
using Runtime.Reward;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace Runtime.Popup
{
    [RequireComponent(typeof(CanvasGroup))]
    public class ClaimPopup : MonoBehaviour
    {
        [SerializeField] private DoTweenSettings animSettings;
        [SerializeField] private TextMeshProUGUI claimText;
        private CanvasGroup _canvasGroup;
        [Inject] private RewardInventory _inventory;
        
        private void OnValidate()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        public void Show()
        {
            gameObject.SetActive(true);
            _canvasGroup.alpha = 0.1f;
            
            _canvasGroup.DOFade(1, animSettings.duration)
                .SetEase(animSettings.ease)
                .SetLink(gameObject);

            claimText.SetText(ExtractTitle());
        }

        private string ExtractTitle()
        {
            var status = _inventory.GetStatus();
            var counter = 0;
            var total = 0;

            foreach (var entry in status)
            {
                counter++;
                total += entry.count;
            }

            return $"Congratz!\nYou have collected {counter} items\n{total} in total";
        }

        public void Hide()
        {
            _canvasGroup.DOFade(0, .2f)
                .SetEase(animSettings.ease)
                .SetLink(gameObject)
                .OnComplete( () => gameObject.SetActive(false));
        }
    }
}