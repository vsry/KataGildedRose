using Kata.GildedRose.Core.Entities;
using Kata.GildedRose.Core.Interfaces;

namespace Kata.GildedRose.Core.Commands
{
    /// <summary>
    /// Process Conjured Items
    /// </summary>
    public class ProcessConjuredItem : IProcessConjuredItem
    {
        public string Execute(Item item)
        {            
            item.DecreaseQuality(2);
            item.DecreaseSellInValue(1);

            return item.ToString();
        }
    }
}
