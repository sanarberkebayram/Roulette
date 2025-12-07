using System;
using DG.Tweening;
using Runtime.Common;
using Runtime.EventBus;
using Runtime.ObjectPooler;
using Runtime.Player.Choice;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Runtime.Player.UI
{
    public class ToggleButton : MonoBehaviour, IPoolObject, IChoiceInteractable
    {
        [Header("Animation Settings")]
        [SerializeField] private DoTweenSettings<float> animSettings = new DoTweenSettings<float>()
        { duration = .2f, ease = Ease.OutSine, target = .7f, };
        
        [Header("References")]
        [SerializeField] private Sprite enabledSprite;
        [SerializeField] private Sprite disabledSprite;
        [SerializeField] private Image buttonImage;
        [SerializeField] private Button button;
        [SerializeField] private TextMeshProUGUI buttonText;

        [Header("Interaction")] 
        [SerializeField] private string interactionId;
        [Inject] private SceneEventBus _eventBus;

        public void Toggle(bool isEnabled,string text)
        {
            button.enabled = isEnabled;
            buttonImage.sprite = isEnabled ? enabledSprite : disabledSprite;
            buttonText.SetText(text);
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
                .DOScale(animSettings.target, animSettings.duration)
                .SetRelative(true)
                .SetLoops(2,LoopType.Yoyo)
                .SetEase(animSettings.ease)
                .SetLink(go) ;
            
            _eventBus.Raise(new OnChoiceRequest() { id = interactionId });
        }

        [Inject]
        private void SetupChoice(ChoiceManager manager)
        {
            var choice = manager.GetChoice(interactionId);
            choice.SetInteractable(this);
        }
        
        #if UNITY_EDITOR
        [ContextMenu("Toggle True")]
        private void ToggleTrue() => Toggle(true,"Test Activated");
        
        [ContextMenu("Toggle False")]
        private void ToggleFalse() => Toggle(false, "Test Deactivated");
        #endif
    }
}