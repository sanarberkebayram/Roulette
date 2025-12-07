using System;
using System.Collections.Generic;
using DG.Tweening;
using Runtime.EventBus;
using Runtime.Wheel;
using Runtime.Zone;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Random = UnityEngine.Random;

namespace Runtime.Common.UI
{
    public class WheelUI : MonoBehaviour
    {
        [Header("References")] 
        [SerializeField] private GameObject animationTarget; 
        [SerializeField] private Image wheelImageTarget;
        [SerializeField] private Image wheelIndicatorTarget;
        [SerializeField] private WheelUISlot[] slots;
        
        [Header("Settings")]
        [SerializeField] private WheelVisualData[] data;
        [SerializeField] private DoTweenSettings<float> idleAnimSettings;
        [SerializeField] private DoTweenSettings<float> spinAnimSettings;
        [SerializeField] private AnimationCurve spinCurve;

        [Inject] private SceneEventBus _eventBus;
        private Dictionary<ZoneType, WheelVisualData> _visualData;

        public Vector3 GetIndexPosition(int index)
        {
            return slots[index].transform.position;
        }

        public void SetWheel(ZoneType zone)
        {
            wheelImageTarget.sprite = _visualData[zone].background;
            wheelIndicatorTarget.sprite = _visualData[zone].indicator;
        }

        public void SetSlot(int index, Sprite icon, string slotText)
        {
            var slot = slots[index];
            slot.SetItem(icon, slotText);
        }

        [ContextMenu("Animate Idle")]
        public void AnimateIdle()
        {
            StopAnimation();
            animationTarget.transform.DORotate(
            idleAnimSettings.target * Vector3.forward,
                    idleAnimSettings.duration
                )
                .SetEase(idleAnimSettings.ease)
                .SetLoops(-1, LoopType.Incremental)
                .SetRelative()
                .SetLink(animationTarget);
        }

        public void AnimateSpin(int index)
        {
            StopAnimation();
            if (slots == null || slots.Length == 0)
                return;

            var perSlotAngle = 360f / slots.Length;
            var slotAngle = perSlotAngle * index;
            var currentAngle = animationTarget.transform.rotation.eulerAngles.z;
            var neededAngle = Mathf.DeltaAngle(currentAngle, slotAngle);
            if (neededAngle < 0f)
                neededAngle += 360f;

            var minSpin = Mathf.Abs(spinAnimSettings.target);
            if (minSpin <= 0f)
                minSpin = 360f;
            
            var totalAngle = neededAngle;
            while (totalAngle < minSpin)
                totalAngle += 360f;
            
            animationTarget.transform.DORotate(
                    totalAngle * Vector3.forward,
                    spinAnimSettings.duration
                )
                .SetOptions(false)
                .SetRelative(true)
                .SetEase(spinCurve)
                .SetLink(animationTarget)
                .OnComplete(() =>
                {
                    _eventBus.Raise(new SpinFinishEvent());
                });
        }

        [ContextMenu("Stop Animation")]
        public void StopAnimation()
        {
            if (!animationTarget)
                return;
            
            animationTarget.transform.DOKill();
        }
        

        void Awake()
        {
            if (data == null)
                return;
            
            _visualData = new Dictionary<ZoneType, WheelVisualData>();
            foreach (var visualData in data)
                _visualData.Add(visualData.zoneType, visualData);
            
        }

        void OnValidate()
        {
            slots = GetComponentsInChildren<WheelUISlot>();
        }
        
        #if UNITY_EDITOR
        [ContextMenu("Start Random Anim")]
        private void StartRandomAnim()
        {
            var randomSlotIndex = Random.Range(0, slots.Length);
            AnimateSpin(randomSlotIndex);
        }

        [ContextMenu("Set Gold")]
        private void SetGold()
        {
            SetWheel(ZoneType.Gold);
        }
        
        [ContextMenu("Set Silver")]
        private void SetSilver()
        {
            SetWheel(ZoneType.Silver);
        }

        [ContextMenu("Set Bronze")]
        private void SetBronze()
        {
            SetWheel(ZoneType.Regular);
        }
        #endif
        
        [Serializable]
        public class WheelVisualData
        {
            public Sprite background;
            public Sprite indicator;
            public ZoneType zoneType;
        }
    }
}
