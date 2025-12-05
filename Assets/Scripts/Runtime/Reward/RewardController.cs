using System;
using System.Collections.Generic;
using System.Linq;
using Runtime.WheelItem;
using Runtime.Zone;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Runtime.Reward
{
    public class RewardController
    {
        private readonly RewardCountData[] _data;
        private readonly ZoneController _zoneController;
        private readonly WheelItemDatabase _wheelItemDatabase;
        
        public RewardController(RewardCountData[] data, ZoneController zoneController, WheelItemDatabase wheelItemDatabase)
        {
            _data = data;
            _zoneController = zoneController;
            _wheelItemDatabase = wheelItemDatabase;
        }

        public SpinInfo GetSpinInfo()
        {
            if (!_zoneController.TryGet(_zoneController.CurrentZoneIndex,out var zone))
                throw new Exception("No zone found");

            var rewards = GetZoneRewards(zone);
            var winner = Random.Range(0, rewards.Length);
            
            return new SpinInfo()
            {
                rewards = rewards,
                winnerIndex = winner,
                bombExploded = winner == 0
            };
        }

        RewardInfo[] GetZoneRewards(ZoneData zone)
        {
            var result = new List<RewardInfo>();
            if (zone.hasBomb)
            {
                result.Add(new RewardInfo()
                {
                    amount = 0 ,
                    uuid = _wheelItemDatabase.GetBomb().uuid
                });
            }

            var addCount = zone.hasBomb ? 7 : 8;
            for (var i = 0; i < addCount; i++)
            {
                var item = GetRandomItemByType(zone.type);
                result.Add(
                    new RewardInfo()
                    {
                        amount = GetRewardCount(zone.displayOrder, item.type),
                        uuid = item.uuid
                    }
                );
            }
            return result.ToArray();
        }

        private WheelItem.WheelItem GetRandomItemByType(ZoneType zoneType)
        {
            WheelItem.WheelItem[] arr;
            Debug.Log(zoneType);
            switch (zoneType)
            {
                case ZoneType.Regular:
                    arr = _wheelItemDatabase.Items.Where(
                        item => item.type == WheelItemType.Regular 
                                || item.type == WheelItemType.Silver)
                        .ToArray();
                    break;
                case ZoneType.Silver:
                    arr = _wheelItemDatabase.Items.Where(
                            item => item.type == WheelItemType.Silver 
                                    || item.type == WheelItemType.Regular || item.type == WheelItemType.Gold)
                        .ToArray();
                    break;
                case ZoneType.Gold:
                    arr = _wheelItemDatabase.Items.Where(
                            item => item.type == WheelItemType.Silver 
                                    || item.type == WheelItemType.Gold)
                        .ToArray();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(zoneType), zoneType, null);
            }
            
            return arr[Random.Range(0, arr.Length)];
        }
        
        private int GetRewardCount(int zoneOrder, WheelItemType itemType)
        {
            var data = _data[zoneOrder];
            Vector2Int interval;
            switch (itemType)
            {
                case WheelItemType.Bomb:
                    interval = Vector2Int.zero;
                    break;
                case WheelItemType.Regular:
                    interval = data.regularItemCountInterval;
                    break;
                case WheelItemType.Silver:
                    interval = data.silverItemCountInterval;
                    break;
                case WheelItemType.Gold:
                    interval = data.goldItemCountInterval;
                    break;
                default:
                    interval = Vector2Int.zero;
                    break;
            }
            
            return Random.Range(interval.x, interval.y + 1);
        }
    }
}