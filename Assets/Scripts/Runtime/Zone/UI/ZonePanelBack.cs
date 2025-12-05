using System.Collections.Generic;
using DG.Tweening;
using Runtime.ObjectPooler;
using UnityEngine;
using UnityEngine.UI;
using Util;
using Zenject;

namespace Runtime.Zone.UI
{
    public class ZonePanelBack : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private ZonePanelAnimationSettings animSettings;
        [SerializeField] private int maxViewers;
        public int MaxViewers => maxViewers;
        
        [Header("References")]
        [SerializeField] private Transform prefabParent;
        [SerializeField] private HorizontalLayoutGroup layout;

        [Inject] private PoolManager _pool;
        private Queue<RegularZoneViewer> _viewers;


        public void Display(ZoneData zone, bool silent = false)
        {
            CompleteAnimation();
            
            if (_viewers.Count >= maxViewers)
                PruneQueue();

            SpawnViewer(ref zone);
            if (silent)
                return;
            
            AnimatePanel();
        }

        public void Clear()
        {
            CompleteAnimation();
            while (_viewers.Count > 0)
                PruneQueue();
        }

        void AnimatePanel()
        {
            var go = layout.gameObject;
            
            var totalOffset = animSettings.offset + layout.spacing;
            layout.padding.right =   (int) -totalOffset;
            
            layout.DORightPadding(0, animSettings.duration)
                .SetEase(animSettings.ease)
                .SetLink(go);
        }

        void SpawnViewer(ref ZoneData zone)
        {
            var viewer = _pool.Get<RegularZoneViewer>();
            viewer.transform.SetParent(prefabParent);
            viewer.transform.localScale = Vector3.one;
            viewer.SetZone(zone);
            _viewers.Enqueue(viewer);
        }

        void CompleteAnimation()
        {
            if (!layout)
                return;
            
            layout.DOComplete();
        }

        void PruneQueue()
        {
            var viewer = _viewers.Dequeue();
            _pool.Release(viewer);
        }

        private void Awake() => _viewers = new Queue<RegularZoneViewer>(maxViewers);
        
        #if UNITY_EDITOR
        
        [ContextMenu("Try Display")]
        private void TryDisplay()
        {
            Display(new ZoneData()
            {
                displayOrder = Random.Range(0,50),
                type = (ZoneType)(Random.Range(0,3))
            });
        }
        
        [ContextMenu("Try DisplaySilent")]
        private void TryDisplaySilent()
        {
            Display(new ZoneData()
            {
                displayOrder = Random.Range(0,50),
                type = (ZoneType)(Random.Range(0,3))
            }, true);
        }
        #endif
    }
}