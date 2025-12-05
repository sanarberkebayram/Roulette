using System;
using DG.Tweening;

namespace Runtime.Zone.UI
{
    [Serializable]
    public struct ZonePanelAnimationSettings
    {
        public float duration;
        public Ease ease;
        public float offset;
    }
}