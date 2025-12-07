using DG.Tweening;
using Runtime.Common;
using Runtime.ObjectPooler;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Reward.UI
{
    public class RewardUISlot : MonoBehaviour, IPoolObject
    {
        [Header("Settings")]
        [SerializeField] private float animDur = .3f;
        [SerializeField] private Ease animEase = Ease.Linear;
        [SerializeField] private DoTweenSettings animSettings;
        
        [Header("References")]
        [SerializeField] private Image itemTarget;
        [SerializeField] private TextMeshProUGUI countTarget;

        public void SetItem(SlotVisualData data)
        {
            itemTarget.sprite = data.icon;
            countTarget.SetText(data.slotText);
        }

        public void UpdateCount(int amount)
        {
            if (!int.TryParse(countTarget.text, out var currentCount))
            {
                currentCount = 0;
            }

            countTarget.DOComplete();
            var go = countTarget.gameObject;

            DOTween.To(() => currentCount,
                    x => countTarget.SetText(x.ToString()),
                    amount,
                    animDur)
                .SetTarget(countTarget)
                .SetLink(go)
                .SetEase(animEase);
        }
        

        public void Release()
        {
            gameObject.SetActive(false);
        }

        public void Get()
        {
            gameObject.SetActive(true);
        }

        public void Destroy()
        {
        }
        
        void OnValidate()
        {
            itemTarget = GetComponentInChildren<Image>();
            countTarget = GetComponentInChildren<TextMeshProUGUI>();
        }
        
        #if UNITY_EDITOR
        [ContextMenu("Try Animation With Random")]
        private void TryAnimationWithRandom()
        {
            UpdateCount(Random.Range(0, 100));
        }
        #endif
    }
}