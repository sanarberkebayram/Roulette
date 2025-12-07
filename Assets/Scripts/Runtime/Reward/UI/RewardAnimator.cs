using UnityEngine;
using DG.Tweening;
using Runtime.Common;
using Runtime.Common.UI;
using Runtime.EventBus;
using Runtime.Item;
using Runtime.ObjectPooler;

namespace Runtime.Reward.UI
{
    public class RewardAnimator
    {
        private readonly float _circularDist;
        private readonly RewardUI _rewardUI;
        private readonly WheelUI _wheelUI;
        private readonly PoolManager _poolManager;
        private readonly SceneEventBus _eventbus;
        private readonly IItemDatabase _itemDatabase;

        private bool _animationSuccess;
        
        public RewardAnimator(RewardUI rewardUI, WheelUI wheelUI, PoolManager poolManager, SceneEventBus eventbus, IItemDatabase itemDatabase, float circularDist)
        {
            _rewardUI = rewardUI;
            _wheelUI = wheelUI;
            _poolManager = poolManager;
            _eventbus = eventbus;
            _itemDatabase = itemDatabase;
            _circularDist = circularDist;
        }

        public void Animate(string itemId, int slotIndex, int count = 5)
        {
            _animationSuccess = false;
            var from = _wheelUI.GetIndexPosition(slotIndex);
            var to = _rewardUI.GetPosition(itemId);

            for (int i = 0; i < count; i++)
            {
                var particle = _poolManager.Get<RewardUISlot>();
                _itemDatabase.TryGetItem(itemId, out var item);
                particle.SetItem(new SlotVisualData() { icon = item.sprite, slotText = string.Empty });
                particle.transform.SetParent(_rewardUI.transform);
                particle.transform.localScale = Vector3.one;
                particle.transform.position = from;
                var offset = (Vector3)(UnityEngine.Random.insideUnitCircle * _circularDist);

                var seq = DOTween.Sequence();
                seq.Append(
                    particle.transform.DOMove(from + offset, .4f)
                        .SetEase(Ease.OutBack)
                );
                seq.Append(
                    particle.transform.DOMove(to, .4f)
                        .SetEase(Ease.InQuad)
                );
                seq.OnComplete(() =>
                {
                    particle.transform.localScale = Vector3.one;
                    particle.transform.SetParent(null);
                    _poolManager.Release(particle);
                    if (!_animationSuccess)
                    {
                        _animationSuccess = true;
                        _eventbus.Raise(new RewardAnimFinishEvent());
                    }
                });
                seq.Play();
            }
        }
    }
}
