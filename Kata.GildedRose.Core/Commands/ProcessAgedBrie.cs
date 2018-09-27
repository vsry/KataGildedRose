using Kata.GildedRose.Core.Entities;
using Kata.GildedRose.Core.Interfaces;

namespace Kata.GildedRose.Core.Commands
{
    /// <summary>
    /// Process Aged Brie
    /// </summary>
    public class ProcessAgedBrie: IProcessAgedBrie
    {
        public string Execute(Item item)
        {
            item.IncreaseQuality(1);
            item.DecreaseSellInValue(1);

            return item.ToString();
        }
    }
}
