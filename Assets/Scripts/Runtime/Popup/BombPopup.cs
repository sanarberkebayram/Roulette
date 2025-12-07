using DG.Tweening;
using Runtime.Player.UI;
using UnityEngine;

namespace Runtime.Popup
{
    [RequireComponent(typeof(CanvasGroup))]
    public class BombPopup : MonoBehaviour
    {
        [SerializeField] private float animDur = 1f;
        [SerializeField] private Ease animEase = Ease.Linear;
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
                .SetLink(gameObject);
        }

        public void Hide()
        {
            _canvasGroup.DOFade(0, .2f)
                .SetEase(animEase)
                .SetLink(gameObject)
                .OnComplete( () => gameObject.SetActive(false));
        }
    }
}