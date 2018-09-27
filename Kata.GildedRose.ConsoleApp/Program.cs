using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Kata.GildedRose.Core.Commands;
using Kata.GildedRose.Core.Entities;
using Kata.GildedRose.Core.Interfaces;
using Kata.GildedRose.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Kata.GildedRose.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //setup our DI
            var serviceProvider = new ServiceCollection()
                .AddLogging()
                .AddSingleton<IInventoryService, InventoryService>()
                .AddSingleton<IProcessAgedBrie, ProcessAgedBrie>()
                .AddSingleton<IProcessBackStagePass, ProcessBackStagePass>()
                .AddSingleton<IProcessConjuredItem, ProcessConjuredItem>()
                .AddSingleton<IProcessInvalidItem, ProcessInvalidItem>()
                .AddSingleton<IProcessNormalItem, ProcessNormalItem>()
                .AddSingleton<IProcessSulfurasItem, ProcessSulfurasItem>()
                .BuildServiceProvider();

            //configure console logging
            serviceProvider.GetService<ILoggerFactory>().AddConsole(LogLevel.Debug);

            var logger = serviceProvider.GetService<ILoggerFactory>().CreateLogger<Program>();

            // Run main app
            App(serviceProvider, logger);
        }

        private static void App(ServiceProvider serviceProvider, ILogger<Program> logger)
        {
            logger.LogInformation("Starting application...");

            // load the service
            var service = serviceProvider.GetService<IInventoryService>();

            // start the metrics
            var sw = new Stopwatch();
            sw.Start();

            // do the work
            // data could be from a file / message queue / db etc, i've just loaded it from a list below in this example
            var result = service.Process(Inventory);

            // stop the metrics
            sw.Stop();

            // update the UI

            // flatten list into single string
            var input = string.Join(Environment.NewLine, Inventory);

            // flatten list into single string
            var output = string.Join(Environment.NewLine, result);

            // create message for user
            var sb = new StringBuilder();
            sb.AppendLine(string.Concat(Enumerable.Repeat("*", 50)));
            sb.AppendLine("Inventory before updating:");            
            sb.AppendLine(input);
            sb.AppendLine(string.Concat(Enumerable.Repeat("*", 50)));
            sb.AppendLine("Inventory after updating:");
            sb.AppendLine(output);
            sb.AppendLine(string.Concat(Enumerable.Repeat("*", 50)));

            // display message
            logger.LogInformation(sb.ToString());

            logger.LogInformation($"Application finished in {sw.ElapsedMilliseconds}ms");

            logger.LogInformation("Press any key to quit.");
            Console.ReadKey();
        }

        public static IList<Item> Inventory = new List<Item>
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
