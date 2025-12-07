using System;
using System.Collections.Generic;
using Runtime.ObjectPooler;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Zone.UI
{
    public class ZoneCenterViewer : MonoBehaviour, IPoolObject
    {
        [Header("References")]
        [SerializeField] private Image bgTarget;
        [SerializeField] private TextMeshProUGUI orderTarget;
        
        [Header("Settings")]
        [SerializeField] private ZoneCenterVisualData[] data;
        private Dictionary<ZoneType, ZoneCenterVisualData> _visualData;

        public void SetZone(ZoneData zoneData)
        {
            bgTarget.sprite = _visualData[zoneData.type].background;
            orderTarget.SetText(zoneData.displayOrder.ToString());
            orderTarget.color = _visualData[zoneData.type].textColor;
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

        void Awake()
        {
            if (data == null)
                return;
            
            _visualData = new Dictionary<ZoneType, ZoneCenterVisualData>();
            foreach (var visualData in data)
                _visualData.Add(visualData.zoneType, visualData);
            
        }
        
        void OnValidate()
        {
            var hashSet = new HashSet<ZoneType>();
            foreach (var visualData in data)
            {
                hashSet.Add(visualData.zoneType);
            }
            if (hashSet.Count != data.Length)
                throw new Exception("Duplicate zone type");
        }
        
#if UNITY_EDITOR
        [ContextMenu("Set Gold")]
        private void SetGold()
        {
            Awake();
            var e = new ZoneData()
            {
                displayOrder = 1,
                type = ZoneType.Gold,
            };
            
            SetZone(e);
        }
        
        [ContextMenu("Set Regular")]
        private void SetRegular()
        {
            Awake();
            var e = new ZoneData()
            {
                displayOrder = 1,
                type = ZoneType.Regular,
            };
            
            SetZone(e);
        }
        
        [ContextMenu("Set Silver")]
        private void SetSilver()
        {
            Awake();
            var e = new ZoneData()
            {
                displayOrder = 1,
                type = ZoneType.Silver,
            };
            
            SetZone(e);
        }
#endif
    }

    [Serializable]
    public class ZoneCenterVisualData
    {
        public Sprite background;
        public Color textColor;
        public ZoneType zoneType;
    }
}