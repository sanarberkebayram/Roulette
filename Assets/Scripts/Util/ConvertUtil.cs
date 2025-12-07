using System;
using Runtime.Common;
using Runtime.Item;
using Runtime.Reward;

namespace Util
{
    public static class ConvertUtil
    {
        public static SpinVisualData ConvertVisuals(this SpinData spinData, IItemDatabase database)
        {
            var slotData = new SlotVisualData[spinData.rewards.Length];

            for (int i = 0; i < spinData.rewards.Length ; i++)
            {
                if (!database.TryGetItem(spinData.rewards[i].uuid, out var item))
                    throw new Exception($"Item ({spinData.rewards[i].uuid}) not found");

                
                slotData[i] = new SlotVisualData()
                {
                    icon = item.sprite,
                    slotText = spinData.rewards[i].isBomb 
                        ? string.Empty 
                        : spinData.rewards[i].count.ToString()
                };
            }

            return new SpinVisualData()
            {
                slotData = slotData,
                winnerIndex = spinData.winnerIndex,
            };
        }
    }
}