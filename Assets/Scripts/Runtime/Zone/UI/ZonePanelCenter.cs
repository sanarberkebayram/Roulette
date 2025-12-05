using DG.Tweening;
using Runtime.ObjectPooler;
using UnityEngine;
using Zenject;

namespace Runtime.Zone.UI
{
    public class ZonePanelCenter : MonoBehaviour
    {
        [Header("Settings")] 
        [SerializeField] private ZonePanelAnimationSettings animSettings;
        
        [Header("References")]
        [SerializeField] private Transform prefabParent;
        
        [Inject] private PoolManager _pool;
        private ZoneCenterViewer _currentViewer;

        public void Display(ZoneData zone, bool silent = false)
        {
            if (!_currentViewer)
                SpawnViewer();

            _currentViewer.SetZone(zone);
            if (silent)
                return;

            AnimateViewer();
        }

        private void AnimateViewer()
        {
            // On Scene Destroy
            if (!_currentViewer)
                return;
            
            _currentViewer.transform.DOKill();
            var go = _currentViewer.gameObject;

            _currentViewer.transform.DOLocalMove(Vector3.zero, animSettings.duration)
                .From(Vector3.right * animSettings.offset)
                .SetEase(animSettings.ease)
                .SetLink(go);
        }

        private void SpawnViewer()
        {
            _currentViewer = _pool.Get<ZoneCenterViewer>();
            _currentViewer.transform.SetParent(prefabParent);
            _currentViewer.transform.localScale = Vector3.one;
            _currentViewer.transform.localPosition = Vector3.zero;
        }
        
        
        #if UNITY_EDITOR
        [ContextMenu("Try Display")]
        private void TryDisplay()
        {
            Display(new ZoneData()
            {
                displayOrder = Random.Range(0,50),
                type = (ZoneType)(Random.Range(0,3))
            }, true);
        }
        [ContextMenu("Try Display Animated")]
        private void TryDisplayAnimated()
        {
            Display(new ZoneData()
            {
                displayOrder = Random.Range(0,50),
                type = (ZoneType)(Random.Range(0,3))
            }, false);
        }
        #endif
    }
}