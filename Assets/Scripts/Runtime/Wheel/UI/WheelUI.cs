using System;
using System.Collections.Generic;
using DG.Tweening;
using Runtime.WheelItem;
using Runtime.Zone;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Zenject;
using Random = UnityEngine.Random;

namespace Runtime.Wheel.UI
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
        [SerializeField] private WheelAnimSettings animSettings;

        [HideInInspector] public UnityEvent onAnimationFinish;
        [Inject] private WheelItemDatabase _database;
        private Dictionary<ZoneType, WheelVisualData> _visualData;

        public void SetWheel(ZoneType zone)
        {
            wheelImageTarget.sprite = _visualData[zone].background;
            wheelIndicatorTarget.sprite = _visualData[zone].indicator;
        }

        [ContextMenu("Animate Idle")]
        public void AnimateIdle()
        {
            StopAnimation();
            animationTarget.transform.DORotate(
            animSettings.idleAngleAmount * Vector3.forward,
                    animSettings.idleDuration
                )
                .SetEase(animSettings.idleEase)
                .SetLoops(-1, LoopType.Incremental)
                .SetRelative()
                .SetLink(animationTarget);
        }

        public void AnimateSpin(int index)
        {
            StopAnimation();
            var perSlotAngle = 360f / slots.Length;
            var slotAngle = perSlotAngle * index * -1f;
            
            var neededAngle = slotAngle - animationTarget.transform.rotation.eulerAngles.z;
            if (neededAngle > animSettings.spinMinAngle * -1f)
            {
                while (neededAngle > animSettings.spinMinAngle * -1f)
                {
                    neededAngle -= 360f;
                }
            }
            
            animationTarget.transform.DORotate(
                    neededAngle * Vector3.forward,
                    animSettings.spinDuration
                )
                .SetOptions(false)
                .SetRelative(true)
                .SetEase(animSettings.spinEase)
                .SetLink(animationTarget)
                .OnComplete(() =>
                {
                    onAnimationFinish?.Invoke();
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
            
            data = null; // Unnecessary memory
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
        public class WheelVisualData // bigger than 15 byte, struct is unnecessary
        {
            public Sprite background;
            public Sprite indicator;
            public ZoneType zoneType;
        }
    }
}