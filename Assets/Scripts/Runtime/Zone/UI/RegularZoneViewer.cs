using System;
using System.Collections.Generic;
using Runtime.ObjectPooler;
using TMPro;
using UnityEngine;

namespace Runtime.Zone.UI
{
    public class RegularZoneViewer : MonoBehaviour, IPoolObject
    {
        [SerializeField] private ZoneCenterVisualData[] data;
        [SerializeField] private TextMeshProUGUI orderTarget;
        
        private Dictionary<ZoneType, ZoneCenterVisualData> _visualData; 


        public void SetZone(ZoneData zone)
        {
            orderTarget.SetText(zone.displayOrder.ToString());
            orderTarget.color = _visualData[zone.type].textColor;
        }
        
        public void Release()
        {
            gameObject.SetActive(false);
            transform.SetParent(null);
        }

        public void Get()
        {
            gameObject.SetActive(true);
        }

        public void Destroy()
        {
        }
        
        private void Awake()
        {
            if (data == null)
                return;
            _visualData = new Dictionary<ZoneType, ZoneCenterVisualData>();
            foreach (var visualData in data)
                _visualData.Add(visualData.zoneType, visualData);
            
            data = null; // Unnecessary memory
        }
        
        [Serializable]
        public class ZoneCenterVisualData
        {
            public Color textColor;
            public ZoneType zoneType;
        }
    }
}