using System;
using DG.Tweening;

namespace Runtime.Common
{
    [Serializable]
    public struct DoTweenSettings<T>
    {
        public float duration;
        public Ease ease;
        public T target;
    }

    [Serializable]
    public struct DoTweenSettings
    {
        public float duration;
        public Ease ease;
    }
}