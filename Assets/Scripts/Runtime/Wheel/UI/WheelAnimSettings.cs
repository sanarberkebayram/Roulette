using System;
using DG.Tweening;
using UnityEngine;

namespace Runtime.Wheel.UI
{
    [Serializable]
    public class WheelAnimSettings // Bigger than 15 bit so struct is unnecessary
    {
        [Header("Idle")]
        public float idleDuration;
        public float idleAngleAmount;
        public Ease idleEase;

        [Header("Spin")]
        public float spinDuration;
        public float spinMinAngle;
        public AnimationCurve spinEase;
    }
}