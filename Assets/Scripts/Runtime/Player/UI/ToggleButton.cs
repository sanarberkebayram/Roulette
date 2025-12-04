using System;
using DG.Tweening;
using Runtime.ObjectPooler;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Player.UI
{
    public class ToggleButton : MonoBehaviour, IPoolObject
    {
        [Header("Animation Settings")]
        [SerializeField] private float animDuration = 0.2f; 
        [SerializeField] private Ease ease = Ease.OutSine;
        [SerializeField] private float animScale = .7f;
        [Header("References")]
        [SerializeField] private Sprite enabledSprite;
        [SerializeField] private Sprite disabledSprite;
        [SerializeField] private Image buttonImage;
        [SerializeField] private Button button;
        [SerializeField] private TextMeshProUGUI buttonText;
        private Action _onClick;

        public void Toggle(bool isEnabled,string text, Action onClick = null)
        {
            _onClick = onClick;
            button.enabled = isEnabled;
            buttonImage.sprite = isEnabled ? enabledSprite : disabledSprite;
            buttonText.SetText(text);
        }
        
        public void Release()
        {
            _onClick = null;
            gameObject.SetActive(false);
        }

        public void Get()
        {
            gameObject.SetActive(true);
        }

        public void Destroy()
        {
        }
        
        private void OnValidate()
        {
            button = GetComponent<Button>();
            buttonText = GetComponentInChildren<TextMeshProUGUI>();
        }

        private void OnEnable()
        {
            button.onClick.AddListener(HandleClick);
        }

        private void OnDisable()
        {
            button.onClick.RemoveListener(HandleClick);
        }
        
        private void HandleClick()
        {
            var go = buttonImage.gameObject;
            go.transform.DOComplete();
            go.transform
                .DOScale(animScale, animDuration)
                .SetRelative(true)
                .SetLoops(2,LoopType.Yoyo)
                .SetEase(ease)
                .SetLink(go) ;
            
            _onClick?.Invoke();
        }
        #if UNITY_EDITOR
        [ContextMenu("Toggle True")]
        private void ToggleTrue() => Toggle(true,"Test Activated");
        
        [ContextMenu("Toggle False")]
        private void ToggleFalse() => Toggle(false, "Test Deactivated");
        #endif
    }
}