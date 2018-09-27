using Kata.GildedRose.Core.Entities;
using Kata.GildedRose.Core.Interfaces;

namespace Kata.GildedRose.Core.Commands
{
    /// <summary>
    /// Process Back Stage Passes
    /// </summary>
    public class ProcessBackStagePass : IProcessBackStagePass
    {
        public string Execute(Item item)
        {
            // if concert happened
            if (item.SellInValue < 0)
            {
                // set quality to 0
                item.DecreaseQuality(item.QualityValue);
            }
            else if (item.SellInValue <= 5)
            {
                item.IncreaseQuality(3);
            }
            else if (item.SellInValue <= 10)
            {
                item.IncreaseQuality(2);
            }
            else
            {
                // ages like Brie, increases with age?
                item.IncreaseQuality(1);
            }

            item.DecreaseSellInValue(1);

            return item.ToString();
        }
    }
}
