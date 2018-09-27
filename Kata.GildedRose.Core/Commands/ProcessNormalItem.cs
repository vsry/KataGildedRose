using Kata.GildedRose.Core.Entities;
using Kata.GildedRose.Core.Interfaces;

namespace Kata.GildedRose.Core.Commands
{
    /// <summary>
    /// Process Normal Items
    /// </summary>
    public class ProcessNormalItem : IProcessNormalItem
    {
        public string Execute(Item item)
        {
            item.DecreaseQuality(1);
            item.DecreaseSellInValue(1);

            return item.ToString();
        }
    }
}
