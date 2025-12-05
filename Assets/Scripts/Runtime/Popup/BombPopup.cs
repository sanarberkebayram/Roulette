using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace Runtime.Popup
{
    [RequireComponent(typeof(CanvasGroup))]
    public class BombPopup : MonoBehaviour
    {
        [SerializeField] private float animDur = 1f;
        [SerializeField] private Ease animEase = Ease.Linear;

        [HideInInspector] public UnityEvent OnPopupFinish;
        private CanvasGroup _canvasGroup;
        
        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        public void Show()
        {
            gameObject.SetActive(true);
            _canvasGroup.alpha = 0.1f;
            
            _canvasGroup.DOFade(1, animDur)
                .SetEase(animEase)
                .SetLoops(2,LoopType.Yoyo)
                .OnComplete(() =>
                {
                    gameObject.SetActive(false);
                    OnPopupFinish?.Invoke();
                }).SetLink(gameObject);
        }
    }
}