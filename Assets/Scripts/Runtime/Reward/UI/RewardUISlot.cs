using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Reward.UI
{
    public class RewardUISlot : MonoBehaviour
    {
        public string Uuid { get; private set; }
        
        [SerializeField] private Image itemTarget;
        [SerializeField] private TextMeshProUGUI countTarget;

        public void SetItem(Sprite item, int count, string uuid)
        {
            itemTarget.sprite = item;
            countTarget.text = count.ToString();
            Uuid = uuid;
        }
        
        void OnValidate()
        {
            itemTarget = GetComponentInChildren<Image>();
            countTarget = GetComponentInChildren<TextMeshProUGUI>();
        }    
    }
}