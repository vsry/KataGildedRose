using System.Collections.Generic;
using Kata.GildedRose.Core.Entities;

namespace Kata.GildedRose.Core.Interfaces

{
    public interface IInventoryService
    {
        /// <summary>
        /// Main method to process all items (the inventory) in a list
        /// </summary>
        /// <param name="items"><see cref="IList{Item}"/></param>
        /// <returns>string output of updated inventorry</returns>
        IEnumerable<string> Process(IList<Item> items);

        /// <summary>
        /// Process individual item
        /// </summary>
        /// <param name="item"><see cref="Item"/></param>
        /// <returns>string output of updated item</returns>
        string ProcessItem(Item item);
    }
}