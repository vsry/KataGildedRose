using Kata.GildedRose.Core.Entities;
using Kata.GildedRose.Core.Interfaces;

namespace Kata.GildedRose.Core.Commands
{
    /// <summary>
    /// Process Sulfuras Items
    /// </summary>
    public class ProcessSulfurasItem : IProcessSulfurasItem
    {
        public string Execute(Item item)
        {
            return item.ToString();
        }
    }
}
