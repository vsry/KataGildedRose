using System;
using System.Collections.Generic;
using System.Linq;
using Kata.GildedRose.Core.Entities;
using Kata.GildedRose.Core.Interfaces;
using Microsoft.Extensions.Logging;

namespace Kata.GildedRose.Core.Services
{
    /// <summary>
    /// Inventory service to update items after a day
    /// </summary>
    public class InventoryService : IInventoryService
    {
        // Logging
        private readonly ILogger<InventoryService> _logger;

        // Item Processors
        private readonly IProcessItem _processInvalidItem;
        private readonly IProcessItem _processAgedBrie;
        private readonly IProcessItem _processBackStagePass;
        private readonly IProcessItem _processConjuredItem;
        private readonly IProcessItem _processNormalItem;
        private readonly IProcessItem _processSulfurasItem;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="logger"><see cref="ILogger{InventoryService}"/></param>
        /// <param name="processInvalidItem"><see cref="IProcessInvalidItem"/></param>
        /// <param name="processAgedBrie"><see cref="IProcessAgedBrie"/></param>
        /// <param name="processBackStagePass"><see cref="IProcessBackStagePass"/></param>
        /// <param name="processConjuredItem"><see cref="IProcessConjuredItem"/></param>
        /// <param name="processNormalItem"><see cref="IProcessNormalItem"/></param>
        /// <param name="processSulfurasItem"><see cref="IProcessSulfurasItem"/></param>
        public InventoryService(
            ILogger<InventoryService> logger,
            IProcessInvalidItem processInvalidItem,
            IProcessAgedBrie processAgedBrie,
            IProcessBackStagePass processBackStagePass,
            IProcessConjuredItem processConjuredItem,
            IProcessNormalItem processNormalItem,
            IProcessSulfurasItem processSulfurasItem
            )
        {            
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            // Item processors
            _processInvalidItem = processInvalidItem ?? throw new ArgumentNullException(nameof(processInvalidItem));
            _processAgedBrie = processAgedBrie ?? throw new ArgumentNullException(nameof(processAgedBrie));
            _processBackStagePass = processBackStagePass ?? throw new ArgumentNullException(nameof(processBackStagePass));
            _processConjuredItem = processConjuredItem ?? throw new ArgumentNullException(nameof(processConjuredItem));
            _processNormalItem = processNormalItem ?? throw new ArgumentNullException(nameof(processNormalItem));
            _processSulfurasItem = processSulfurasItem ?? throw new ArgumentNullException(nameof(processSulfurasItem));
        }

        /// <summary>
        /// Main method to process all items (the inventory) in a list
        /// </summary>
        /// <param name="items"><see cref="IList{Item}"/></param>
        /// <returns>string output of updated inventory</returns>
        public IEnumerable<string> Process(IList<Item> items)
        {
            if (items == null) throw new ArgumentNullException(nameof(items));

            _logger.LogTrace($"Found {items.Count} to process");

            // LINQ to loop through each item and pass it to ProcessItem()
            return items.Select(ProcessItem);
        }

        /// <summary>
        /// Process individual item
        /// </summary>
        /// <param name="item"><see cref="Item"/></param>
        /// <returns>string output of updated item</returns>
        public string ProcessItem(Item item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item), "Item must not be null");

            _logger.LogTrace($"Processing Item {item.Name}");

            switch (item.Name.ToLowerInvariant())
            {
                case "conjured":
                    return _processConjuredItem.Execute(item);
                case "aged brie":
                    return _processAgedBrie.Execute(item);
                case "normal item":
                    return _processNormalItem.Execute(item);
                case "sulfuras":
                    return _processSulfurasItem.Execute(item);
                case "backstage passes":
                    return _processBackStagePass.Execute(item);
                default:
                    return _processInvalidItem.Execute(item);
            }
        }       
    }
}
