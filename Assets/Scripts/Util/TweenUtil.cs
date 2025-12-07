using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Util
{
    public static class TweenUtil
    {
        public static Tweener DoRightPadding(this HorizontalLayoutGroup layout, int toValue, float duration)
        {
            var fromValue = layout.padding.right;
            Tweener t = DOTween.To(
                    () => (float)fromValue,
                    x =>
                    {
                        var v = Mathf.RoundToInt(x);
                        layout.padding.right = v;
                        LayoutRebuilder.MarkLayoutForRebuild((RectTransform)layout.transform);
                    },
                    toValue,
                    duration
                )
                .SetTarget(layout);

            return t;
        }
    }
}