using System;
using System.Collections.Generic;
using Runtime.Item;
using Runtime.Reward;
using Runtime.Zone;
using Runtime.Zone.Provider;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Runtime.Reward.Strategy
{
    [CreateAssetMenu(fileName = "ProceduralRewarder", menuName = "Reward/Procedural", order = 1)]
    public class ProceduralRewardSo : ScriptableObject
    {
        [Header("Sources")] 
        [SerializeField] private ScriptableObject zoneProviderSo;
        [SerializeField] private ItemDatabaseSo itemDatabase;

        [Header("General")]
        [SerializeField, Min(1)] private int slotsPerSpin = 12;
        [SerializeField] private BombSettings bombSettings;

        [Header("Item Pools")]
        [SerializeField] private RewardItemPool regularPool;
        [SerializeField] private RewardItemPool silverPool;
        [SerializeField] private RewardItemPool goldPool;
        
        [Header("Zone Distributions")]
        [SerializeField] private ZoneRewardDistribution regularZoneDistribution;
        [SerializeField] private ZoneRewardDistribution silverZoneDistribution;
        [SerializeField] private ZoneRewardDistribution goldZoneDistribution;

        public int SlotsPerSpin => Mathf.Max(1, slotsPerSpin);
        private readonly Dictionary<ItemType, ItemDefinition[]> _itemPools = new();
        public ZoneRewardDistribution GetDistribution(ZoneType zoneType)
        {
            return zoneType switch
            {
                ZoneType.Regular => regularZoneDistribution,
                ZoneType.Silver => silverZoneDistribution,
                ZoneType.Gold => goldZoneDistribution,
                _ => regularZoneDistribution
            };
        }

        public bool TryGetZoneProvider(out IZoneProvider provider)
        {
            provider = zoneProviderSo as IZoneProvider;
            return provider != null;
        }

        RewardItemPool GetPoolSettings(ItemType type)
        {
            return type switch
            {
                ItemType.Regular => regularPool,
                ItemType.Silver => silverPool,
                ItemType.Gold => goldPool,
                _ => regularPool
            };
        }

        public bool TryCreateBombReward(out RewardData reward)
        {
            if (bombSettings != null && bombSettings.IsConfigured)
            {
                reward = bombSettings.CreateReward();
                return true;
            }

            reward = default;
            return false;
        }

        public SpinData CreateSpinData(ZoneData zone)
        {
            var rewards = new RewardData[SlotsPerSpin];
            var distribution = GetDistribution(zone.type);

            for (var i = 0; i < rewards.Length; i++)
            {
                var itemType = distribution != null
                    ? distribution.GetRandomItemType()
                    : ItemType.Regular;
                var pool = GetPoolSettings(itemType);

                if (pool == null || !TryGetItemDefinition(itemType, out var definition))
                {
                    Debug.LogError($"Reward pool for {itemType} is empty.", this);
                    rewards[i] = default;
                    continue;
                }

                rewards[i] = new RewardData
                {
                    uuid = definition.uuid,
                    count = pool.GetRandomAmount(),
                    isBomb = false
                };
            }

            if (zone.hasBomb)
            {
                if (TryCreateBombReward(out var bombReward))
                {
                    var bombIndex = Random.Range(0, rewards.Length);
                    rewards[bombIndex] = bombReward;
                }
                else
                {
                    Debug.LogWarning("Zone requires a bomb reward but no bomb item is configured.", this);
                }
            }

            return new SpinData
            {
                rewards = rewards,
                winnerIndex = Random.Range(0, rewards.Length)
            };
        }

        private bool TryGetItemDefinition(ItemType itemType, out ItemDefinition definition)
        {
            var pool = GetItemPool(itemType);
            if (pool.Length == 0)
            {
                definition = default;
                return false;
            }

            var index = Random.Range(0, pool.Length);
            definition = pool[index];
            return true;
        }

        private ItemDefinition[] GetItemPool(ItemType itemType)
        {
            if (_itemPools.TryGetValue(itemType, out var cached))
                return cached ?? Array.Empty<ItemDefinition>();

            var built = BuildItemPool(itemType);
            _itemPools[itemType] = built;
            return built;
        }

        private ItemDefinition[] BuildItemPool(ItemType itemType)
        {
            if (itemDatabase == null || itemDatabase.Items == null || itemDatabase.Items.Count == 0)
                return Array.Empty<ItemDefinition>();

            var list = new List<ItemDefinition>();
            foreach (var definition in itemDatabase.Items)
            {
                if (definition.itemType == itemType && !string.IsNullOrEmpty(definition.uuid))
                    list.Add(definition);
            }

            return list.ToArray();
        }

        private void OnValidate()
        {
            _itemPools.Clear();

            if (zoneProviderSo != null && zoneProviderSo is not IZoneProvider)
            {
                zoneProviderSo = null;
                Debug.LogError($"{nameof(IZoneProvider)} could not be found.");
            }

            if (itemDatabase == null)
                Debug.LogWarning("Item database is not set for ProceduralRewarder.", this);
        }
    }

    [Serializable]
    public class RewardItemPool
    {
        [SerializeField] private Vector2Int rewardCountRange = new Vector2Int(1, 1);

        public int GetRandomAmount()
        {
            var min = Mathf.Max(0, rewardCountRange.x);
            var max = Mathf.Max(min, rewardCountRange.y);
            if (min == max)
                return min;

            return Random.Range(min, max + 1);
        }
    }

    [Serializable]
    public class ZoneRewardDistribution
    {
        [SerializeField] private ItemTypeChance[] itemTypes = Array.Empty<ItemTypeChance>();

        public ItemType GetRandomItemType()
        {
            if (itemTypes == null || itemTypes.Length == 0)
                return ItemType.Regular;

            var totalWeight = 0f;
            foreach (var type in itemTypes)
                totalWeight += Mathf.Max(0f, type.weight);

            if (totalWeight <= 0f)
                return itemTypes[0].itemType;

            var roll = Random.value * totalWeight;
            foreach (var type in itemTypes)
            {
                var weight = Mathf.Max(0f, type.weight);
                if (roll <= weight)
                    return type.itemType;

                roll -= weight;
            }

            return itemTypes[itemTypes.Length - 1].itemType;
        }
    }

    [Serializable]
    public struct ItemTypeChance
    {
        public ItemType itemType;
        [Min(0f)] public float weight;
    }

    [Serializable]
    public class BombSettings
    {
        [SerializeField] private ItemDefinition item;
        [SerializeField] private int bombCount;

        public bool IsConfigured => !string.IsNullOrEmpty(item.uuid);

        public RewardData CreateReward()
        {
            return new RewardData
            {
                uuid = item.uuid,
                count = bombCount,
                isBomb = true
            };
        }
    }
}
