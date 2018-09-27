using System;

namespace Kata.GildedRose.Core.Entities
{
    /// <summary>
    /// Inventory Item
    /// </summary>
    public class Item
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="name">Name of Item</param>
        /// <param name="sellInValue">Sell in value</param>
        /// <param name="qualityValue">Quality value</param>
        public Item(string name, int sellInValue, int qualityValue)
        {
            // Guard statements
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name), "Must have a name");

            _originalQualityValue = qualityValue;

            if (qualityValue > 50) qualityValue = 50;

            // assign values
            SellInValue = sellInValue;
            QualityValue = qualityValue;
            Name = name;
        }

        private readonly int _originalQualityValue;

        /// <summary>
        /// String version of Item
        /// </summary>
        public override string ToString()
        {
            return $"{Name} {SellInValue} {QualityValue}";
        }

        /// <summary>
        /// Name of Item
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Sell in value - number of days we have to sell the item
        /// </summary>
        public int SellInValue { get; private set; }

        /// <summary>
        /// How valuable the item is
        /// </summary>
        public int QualityValue { get; private set; }

        /// <summary>
        /// Decrease quality value of item
        /// </summary>
        /// <param name="decreaseBy">int of how much to decrease by</param>
        public void DecreaseQuality(int decreaseBy)
        {
            // if OriginalQuality > 50, then don't decrease, just return 50
            if (_originalQualityValue > 50) return;

            // if sell by date in past, then degrade twice as fast
            if (SellInValue < 0)
            {
                decreaseBy = decreaseBy * 2;
            }

            QualityValue = QualityValue - decreaseBy;

            if (QualityValue < 0) QualityValue = 0;
        }

        /// <summary>
        /// Increase quality value of ite
        /// </summary>
        /// <param name="increaseBy">int of how much to increase by</param>
        public void IncreaseQuality(int increaseBy)
        {
            QualityValue = QualityValue + increaseBy;

            if (QualityValue > 50) QualityValue = 50;
        }

        /// <summary>
        /// Decrease sell in value
        /// </summary>
        /// <param name="decreaseBy">int of how much to decrease by</param>
        public void DecreaseSellInValue(int decreaseBy)
        {
            SellInValue = SellInValue - decreaseBy;
        }
    }
}
