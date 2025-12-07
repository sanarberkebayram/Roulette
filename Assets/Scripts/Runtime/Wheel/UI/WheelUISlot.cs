using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Common.UI
{
    public class WheelUISlot : MonoBehaviour
    {
        [SerializeField] private Image itemTarget;
        [SerializeField] private TextMeshProUGUI countTarget;

        public void SetItem(Sprite icon, string slotText)
        {
            itemTarget.sprite = icon;
            countTarget.SetText(slotText);
        }
        
        void OnValidate()
        {
            itemTarget = GetComponentInChildren<Image>();
            countTarget = GetComponentInChildren<TextMeshProUGUI>();
        }
    }
}