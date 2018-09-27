using System.Collections.Generic;
using FluentAssertions;
using Kata.GildedRose.Core.Commands;
using Kata.GildedRose.Core.Entities;
using Kata.GildedRose.Core.Services;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Kata.GildedRose.Tests
{
    public class AcceptanceTests
    { 
        [Fact]
        public void AcceptanceTest()
        {
            // arrange
            var loggerMoq = new Mock<ILogger<InventoryService>>();

            var processAgedBrie = new ProcessAgedBrie();
            var processBackStagePass = new ProcessBackStagePass();
            var processConjuredItem = new ProcessConjuredItem();
            var processInvalidItem = new ProcessInvalidItem();
            var processNormalItem = new ProcessNormalItem();
            var processSulfurasItem = new ProcessSulfurasItem();

            var expected = new List<string>
            {
                "Aged Brie 0 2",
                "Backstage passes -2 0",
                "Backstage passes 8 4",
                "Sulfuras 2 2",
                "Normal Item -2 50",
                "Normal Item 1 1",
                "NO SUCH ITEM",
                "Conjured 1 0",
                "Conjured -2 1"
            };

            var svc = new InventoryService(loggerMoq.Object, processInvalidItem, processAgedBrie, processBackStagePass, processConjuredItem, processNormalItem, processSulfurasItem);

            // act 
            var actual = svc.Process(Inventory);

            // assert
            actual.Should().BeEquivalentTo(expected);

        }

        public IList<Item> Inventory = new List<Item>
        {
            new Item("Aged Brie", 1, 1),
            new Item("Backstage passes", -1, 2),
            new Item("Backstage passes", 9, 2),
            new Item("Sulfuras", 2, 2),
            new Item("Normal Item", -1, 55),
            new Item("Normal Item", 2, 2),
            new Item("INVALID ITEM", 2, 2),
            new Item("Conjured", 2, 2),
            new Item("Conjured", -1, 5)
        };
    }
}
