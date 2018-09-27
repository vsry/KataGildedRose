using System;
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
    public class UnitTests
    {
        [Fact]
        public void GIVEN_InventoryService_WHEN_ProcessingWithNullItems_THEN_ThrowsArgumentNullException()
        {
            // arrange
            List<Item> items = null;

            var loggerMoq = new Mock<ILogger<InventoryService>>();

            var processAgedBrie = new ProcessAgedBrie();
            var processBackStagePass = new ProcessBackStagePass();
            var processConjuredItem = new ProcessConjuredItem();
            var processInvalidItem = new ProcessInvalidItem();
            var processNormalItem = new ProcessNormalItem();
            var processSulfurasItem = new ProcessSulfurasItem();

            var svc = new InventoryService(loggerMoq.Object, processInvalidItem, processAgedBrie, processBackStagePass, processConjuredItem, processNormalItem, processSulfurasItem);

            // act 
            Action act = () => svc.Process(items); 

            // assert
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void GIVEN_InventoryService_WHEN_ProcessingWithNullItem_THEN_ThrowsArgumentNullException()
        {
            // arrange
            Item item = null;

            var loggerMoq = new Mock<ILogger<InventoryService>>();

            var processAgedBrie = new ProcessAgedBrie();
            var processBackStagePass = new ProcessBackStagePass();
            var processConjuredItem = new ProcessConjuredItem();
            var processInvalidItem = new ProcessInvalidItem();
            var processNormalItem = new ProcessNormalItem();
            var processSulfurasItem = new ProcessSulfurasItem();

            var svc = new InventoryService(loggerMoq.Object, processInvalidItem, processAgedBrie, processBackStagePass, processConjuredItem, processNormalItem, processSulfurasItem);

            // act 
            Action act = () => svc.ProcessItem(item);

            // assert
            act.Should().Throw<ArgumentNullException>();
        }


        [Fact]
        public void GIVEN_AgedBrie_WHEN_Processed_THEN_QualityShouldIncreaseBy1()
        {
            // arrange
            const string expected = "Aged Brie 0 2";
            var item = new Item("Aged Brie", 1, 1);

            // act
            var svc = new ProcessAgedBrie();
            var actual = svc.Execute(item);

            // assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void GIVEN_NormalItem_WHEN_Processed_THEN_QualityShouldDecreaseBy1()
        {
            // arrange
            const string expected = "Normal Item 1 1";
            var item = new Item("Normal Item", 2, 2);

            // act
            var svc = new ProcessNormalItem();
            var actual = svc.Execute(item);

            // assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void GIVEN_ConjuredItem_WHEN_Processed_THEN_QualityShouldDecreaseBy2()
        {
            // arrange
            const string expected = "Conjured Item 2 2";
            var item = new Item("Conjured Item", 3, 4);

            // act
            var svc = new ProcessConjuredItem();
            var actual = svc.Execute(item);

            // assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void GIVEN_AnyItem_WHEN_CreatedWithQualityValueOf55_THEN_QualityShouldBe50()
        {
            // arrange
            const string expected = "Normal Item -1 50";

            // act
            var item = new Item("Normal Item", -1, 55);
            var actual = item.ToString();

            // assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void GIVEN_AnyItem_WHEN_CreatedWithQualityValueOf50AndIncremented_THEN_QualityShouldBe50()
        {
            // arrange
            const string expected = "Normal Item -1 50";
            var item = new Item("Normal Item", -1, 50);

            // act
            item.IncreaseQuality(1);

            var actual = item.ToString();

            // assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void GIVEN_AnyItem_WHEN_CreatedNullName_THEN_ThrowsArgumentNullException()
        {
            // arrange
            // act 
            Action act = () => new Item(null, 1, 1);

            // assert
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void GIVEN_Sulfuras_WHEN_Processed_THEN_Nothing_Changes()
        {
            // arrange
            const string expected = "Sulfuras 4 4";
            var item = new Item("Sulfuras", 4, 4);

            // act
            var svc = new ProcessSulfurasItem();
            var actual = svc.Execute(item);

            // assert
            actual.Should().BeEquivalentTo(expected);
        }


        [Fact]
        public void GIVEN_BackstagePasses_WHEN_Processed_And_SellInEqualTo10_THEN_QualityIncreasesBy2()
        {
            // arrange
            const string expected = "Backstage Passes 9 6";
            var item = new Item("Backstage Passes", 10, 4);

            // act
            var svc = new ProcessBackStagePass();
            var actual = svc.Execute(item);

            // assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void GIVEN_BackstagePasses_WHEN_Processed_And_SellInEqualTo5_THEN_QualityIncreasesBy3()
        {
            // arrange
            const string expected = "Backstage Passes 4 7";
            var item = new Item("Backstage Passes", 5, 4);

            // act
            var svc = new ProcessBackStagePass();
            var actual = svc.Execute(item);

            // assert
            actual.Should().BeEquivalentTo(expected);
        }


        [Fact]
        public void GIVEN_BackstagePasses_WHEN_Processed_And_SellInLessThan1_THEN_QualitySetTo0()
        {
            // arrange
            const string expected = "Backstage Passes -2 0";
            var item = new Item("Backstage Passes", -1, 4);

            // act
            var svc = new ProcessBackStagePass();
            var actual = svc.Execute(item);

            // assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void GIVEN_BackstagePasses_WHEN_Processed_And_SellInGreaterThan10_THEN_QualityIncreases()
        {
            // arrange
            const string expected = "Backstage Passes 14 5";
            var item = new Item("Backstage Passes", 15, 4);

            // act
            var svc = new ProcessBackStagePass();
            var actual = svc.Execute(item);

            // assert
            actual.Should().BeEquivalentTo(expected);
        }
    }
}
