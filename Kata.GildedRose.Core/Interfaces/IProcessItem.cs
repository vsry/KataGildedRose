using Kata.GildedRose.Core.Entities;

namespace Kata.GildedRose.Core.Interfaces
{
    /// <summary>
    /// Base interface for processing items
    /// </summary>
    public interface IProcessItem
    {
        string Execute(Item item);
    }   
}