using Kata.GildedRose.Core.Entities;
using Kata.GildedRose.Core.Interfaces;

namespace Kata.GildedRose.Core.Commands
{
    /// <summary>
    /// Process Invalid Items
    /// </summary>
    public class ProcessInvalidItem : IProcessInvalidItem
    {
        public string Execute(Item item)
        {
            return "NO SUCH ITEM";
        }
    }
}
