using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Wheel.UI
{
    public class WheelUISlot : MonoBehaviour
    {
        [SerializeField] private Image itemTarget;
        [SerializeField] private TextMeshProUGUI countTarget;

        public void SetItem(Sprite item, int count)
        {
            itemTarget.sprite = item;
            countTarget.text = count.ToString();
        }
        
        void OnValidate()
        {
            itemTarget = GetComponentInChildren<Image>();
            countTarget = GetComponentInChildren<TextMeshProUGUI>();
        }
    }
}